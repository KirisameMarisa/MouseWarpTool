using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouseWarpTool.Models
{
    public static class Display
    {
        public static System.Windows.Forms.Screen GetMainDisplay()
        {
            return System.Windows.Forms.Screen.AllScreens[0];
        }

        public static IEnumerable<System.Windows.Forms.Screen> GetSubDisplay()
        {
            for (int i = 1; i < System.Windows.Forms.Screen.AllScreens.Length; ++i)
            {
                yield return System.Windows.Forms.Screen.AllScreens[i];
            }
        }

        public static Point GetCenterPoint(System.Drawing.Rectangle bounds)
        {
            return new Point
            {
                X = bounds.Top + (bounds.Height / 2) - 150,
                Y = bounds.Left + (bounds.Width / 2) - 150
            };
        }
    }
}
