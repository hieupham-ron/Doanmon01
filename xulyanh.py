import cv2
import numpy as np
import math
import socket
import struct
import time

# ==========================================
# CẤU HÌNH
# ==========================================
SERVER_IP = "127.0.0.1"
VIDEO_PORT = 8889
MIN_AREA = 800
SHOW_DEBUG_WINDOWS = False   # True nếu muốn hiện thêm cửa sổ OpenCV để kiểm tra

# ==========================================
# MỞ CAMERA
# ==========================================
cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
if not cap.isOpened():
    cap = cv2.VideoCapture(0)

cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)

if not cap.isOpened():
    print("Khong mo duoc camera!")
    raise SystemExit


# ==========================================
# HÀM PHÂN LOẠI HÌNH DẠNG
# ==========================================
def classify_shape(contour):
    area = cv2.contourArea(contour)
    perimeter = cv2.arcLength(contour, True)
    if area <= 0 or perimeter <= 0:
        return "Khong xac dinh"

    approx = cv2.approxPolyDP(contour, 0.02 * perimeter, True)
    vertices = len(approx)
    x, y, w, h = cv2.boundingRect(contour)
    if w <= 0 or h <= 0:
        return "Khong xac dinh"

    ratio = min(w, h) / max(w, h)
    circularity = 4 * math.pi * area / (perimeter * perimeter)
    area_ratio = area / (w * h)

    if vertices >= 5 and circularity >= 0.75 and area_ratio >= 0.65:
        return "Hinh tron"
    if vertices == 3:
        return "Tam giac"
    if vertices == 4:
        return "Hinh vuong" if ratio >= 0.88 else "Hinh chu nhat"

    return "Khong xac dinh"


# ==========================================
# KẾT NỐI TCP TỚI C# (tự thử lại đến khi được)
# ==========================================
def connect_to_csharp():
    while True:
        s = None
        try:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.connect((SERVER_IP, VIDEO_PORT))
            s.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)
            print("Da ket noi thanh cong toi C# Stream Server!")
            return s
        except Exception:
            if s is not None:
                try:
                    s.close()
                except Exception:
                    pass
            print("Dang cho C# (Form2 phai dang mo)...")
            time.sleep(1)


print("Dang cho ket noi TCP toi phan mem C#...")
sock = connect_to_csharp()

# ==========================================
# VÒNG LẶP CHÍNH
# ==========================================
while True:
    ret, frame = cap.read()
    if not ret:
        print("Khong doc duoc frame tu camera")
        break

    # ---- Xử lý ảnh ----
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)
    _, thresh = cv2.threshold(blurred, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)

    kernel = np.ones((3, 3), np.uint8)
    thresh = cv2.morphologyEx(thresh, cv2.MORPH_OPEN, kernel, iterations=1)
    thresh = cv2.morphologyEx(thresh, cv2.MORPH_CLOSE, kernel, iterations=2)

    contours, _ = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    contours = sorted(contours, key=cv2.contourArea, reverse=True)

    detected = ""   # hình hợp lệ lớn nhất trong khung này

    for contour in contours:
        area = cv2.contourArea(contour)
        if area < MIN_AREA:
            continue

        x, y, w, h = cv2.boundingRect(contour)
        shape_name = classify_shape(contour)

        if detected == "" and shape_name != "Khong xac dinh":
            detected = shape_name

        cv2.drawContours(frame, [contour], -1, (0, 255, 0), 2)
        cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 255), 2)
        cv2.putText(frame, shape_name, (x, max(y - 10, 25)),
                    cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 2)

    # ---- Gửi sang C#: [4 byte size ảnh][2 byte size meta][meta][ảnh] ----
    try:
        ok, encoded_img = cv2.imencode('.jpg', frame, [int(cv2.IMWRITE_JPEG_QUALITY), 80])
        if ok:
            data = encoded_img.tobytes()
            meta = detected.encode("utf-8")
            sock.sendall(struct.pack(">LH", len(data), len(meta)) + meta + data)
    except Exception:
        try:
            sock.close()
        except Exception:
            pass
        sock = connect_to_csharp()

    # ---- Cửa sổ kiểm tra (tùy chọn) ----
    if SHOW_DEBUG_WINDOWS:
        cv2.imshow("Python Camera Stream", frame)
        cv2.imshow("Anh nhi phan", thresh)
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break
    else:
        time.sleep(0.01)

# ==========================================
# DỌN DẸP
# ==========================================
try:
    sock.close()
except Exception:
    pass
cap.release()
cv2.destroyAllWindows()