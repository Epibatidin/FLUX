using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameHandler;
using System.IO;


namespace MP3Tags
{
    class Program
    {

        static void Main(string[] args)
        {
            string baseDir = @"D:\Test\Bilder\{0}.mp3";
            string baseDir2 = @"D:\Test\{0}.mp3";
            string Target = @"D:\Test\Target\{0}.mp3";

            string filePath = String.Format(baseDir2, "2");

            FileInfo info = new FileInfo(filePath);
            if(!info.Exists)
                throw new FileNotFoundException();
            
            ContainerFactory fac = new ContainerFactory();
            var cont = fac.Create(info.OpenRead());
            cont.ReadFrameCollection();

            foreach (var item in cont.Frames)
            {
                System.Console.WriteLine(item.FrameID + " => " + item.FrameData);
            }

            string targetDir = String.Format(Target, 3);
            cont.CopyTo(targetDir);

            FileInfo info2 = new FileInfo(targetDir);
            var cont2 = fac.Create(info2.OpenRead());

            cont2.ReadFrameCollection();

            foreach (var item in cont2.Frames)
            {
                System.Console.WriteLine(item.FrameID + " => " + item.FrameData);
            }
            System.Console.ReadKey();
        }


        static void WriteFile(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            
            while(true)
            {
                var value = fs.ReadByte();
                if (value == -1) break;

                System.Console.Write(value + " ");
            }
            System.Console.ReadKey();
                       

        }


    }
}
