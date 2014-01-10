using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ImageHandling
{
    public class Blub
    {
        public void bla(Stream s)
        {
            Bitmap map = new Bitmap(s);
            map.Save("d:\test\bla.png", ImageFormat.Png);
        }


        
        


    }
}
