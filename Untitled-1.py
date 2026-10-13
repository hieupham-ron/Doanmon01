import cv2
import numpy as np

# Khởi động camera
cap = cv2.VideoCapture(0, cv2.CAP_MSMF)

if not cap.isOpened():
  print("Không mở được camera!")
  exit()

print("Đang chạy bước 3.3: Nhận dạng phôi thật... Nhấn 'q' để thoát.")

# Hệ số quy đổi từ Pixel sang mm
PIXEL_TO_MM = 0.2

while True:
  ret, frame = cap.read()
  if not ret:
    print("Không đọc được ảnh!")
    break

  gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
  blurred = cv2.GaussianBlur(gray, (7, 7), 0)

  thresh = cv2.adaptiveThreshold(
      blurred, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY_INV, 11, 2
  )

  kernel = np.ones((3, 3), np.uint8)
  thresh = cv2.morphologyEx(thresh, cv2.MORPH_OPEN, kernel, iterations=1)
  thresh = cv2.morphologyEx(thresh, cv2.MORPH_CLOSE, kernel, iterations=1)

  contours, _ = cv2.findContours(
      thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE
  )

  for cnt in contours:
    area = cv2.contourArea(cnt)

    if area > 1200:
      rect = cv2.minAreaRect(cnt)
      box = cv2.boxPoints(rect)
      box = box.astype(int)  # Đã sửa ở đây để tương thích với NumPy mới

      (w_px, h_px) = rect[1]
      if w_px > 0 and h_px > 0:
        width_mm = min(w_px, h_px) * PIXEL_TO_MM
        length_mm = max(w_px, h_px) * PIXEL_TO_MM

        perimeter = cv2.arcLength(cnt, True)
        approx = cv2.approxPolyDP(cnt, 0.03 * perimeter, True)
        vertices = len(approx)

        shape_name = "Phoi lạ"
        if vertices == 3:
          shape_name = "Phoi Tam Giac"
        elif vertices == 4:
          aspect_ratio = float(min(w_px, h_px)) / max(w_px, h_px)
          if 0.9 <= aspect_ratio <= 1.1:
            shape_name = "Phoi Vuong"
          else:
            shape_name = "Phoi Chu Nhat"
        elif vertices > 4:
          area_ratio = area / (w_px * h_px)
          if area_ratio > 0.7:
            shape_name = "Phoi Tron"

        cv2.drawContours(frame, [box], 0, (0, 255, 255), 2)

        x, y = box[0]
        text_shape = f"{shape_name}"
        text_size = f"{width_mm:.1f}x{length_mm:.1f} mm"

        cv2.putText(
            frame,
            text_shape,
            (int(x), int(y) - 15),
            cv2.FONT_HERSHEY_SIMPLEX,
            0.6,
            (0, 255, 0),
            2,
        )
        cv2.putText(
            frame,
            text_size,
            (int(x), int(y) + 10),
            cv2.FONT_HERSHEY_SIMPLEX,
            0.5,
            (255, 255, 0),
            2,
        )

  cv2.imshow("3.3 - Nhan dang phoi that (Smart Sorting)", frame)
  cv2.imshow("Adaptive Threshold (Xu ly anh that)", thresh)

  if cv2.waitKey(1) & 0xFF == ord("q"):
    break

cap.release()
cv2.destroyAllWindows()