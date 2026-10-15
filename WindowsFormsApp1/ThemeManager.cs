using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class ThemeManager
    {
        // Palettes (you can add more)
        public static Color WindowBackground => Color.FromArgb(245, 247, 250);
        public static Color PanelBackground => Color.White;
        public static Color PrimaryAccent => Color.FromArgb(30, 144, 255); // DodgerBlue
        public static Color ButtonNormal => Color.FromArgb(230, 245, 255);
        public static Color ButtonHover => Color.FromArgb(200, 230, 255);
        public static Color SlotEmpty => SystemColors.Control;
        public static Color SlotOccupied => Color.LightGreen;
        public static Color SlotSelected => Color.LightSkyBlue;
        public static Color Foreground => Color.FromArgb(40, 40, 40);

        public static void ApplyThemeToForm(Form form)
        {
            form.BackColor = WindowBackground;
            ApplyThemeRecursive(form);
        }

        private static void ApplyThemeRecursive(Control control)
        {
            // Apply basic rules
            if (control is GroupBox || control is Panel)
            {
                control.BackColor = PanelBackground;
                control.ForeColor = Foreground;
            }
            if (control is Button btn)
            {
                btn.BackColor = ButtonNormal;
                btn.ForeColor = Foreground;
                btn.FlatStyle = FlatStyle.Flat;
            }
            if (control is Label lbl)
            {
                lbl.ForeColor = Foreground;
            }
            if (control is ComboBox cb)
            {
                cb.BackColor = PanelBackground;
                cb.ForeColor = Foreground;
            }
            // recurse children
            foreach (Control child in control.Controls)
            {
                ApplyThemeRecursive(child);
            }
        }
    }
}
