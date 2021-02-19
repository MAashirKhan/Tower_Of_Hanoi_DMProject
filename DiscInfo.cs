using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_Of_Hanoi_DMProject
{
    //Class For Disc Information
    public class DiscInfo
    {
        public int width;
        public int height;
        public Color color;
        public int number;
        public Brush brush;

        // Constructor For Creating Disc
        public DiscInfo(int w, int h, Color c, int n)
        {
            height = h;
            width = w;
            color = c;
            number = n;
            brush = new SolidBrush(color);
        }

        
    }
}
