using DevComponents.DotNetBar;
using System;
using System.Windows.Forms;

namespace QuanLyTruongCap3.Components
{
    public class MyCursors : UserControl
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern IntPtr LoadCursorFromFile(string str);

        public static Cursor Create(string filename)
        {
            IntPtr cursor = LoadCursorFromFile(filename);

            if (!IntPtr.Zero.Equals(cursor))
                return new Cursor(cursor);
            MessageBoxEx.Show("Không thể tạo con trỏ chuột từ file Pointer.cur!\nCó thể file này bị lỗi hoặc không tồn tại trong hệ thống.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return Cursors.Default;
        }
    }
}