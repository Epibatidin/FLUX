using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FrameHandler;

namespace MP3TagReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = @"F:\FluxWorkBenchFiles\ComponentTests\4.mp3";

            FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);

            ContainerFactory fac = new ContainerFactory();

            var container = fac.Create(fs);

            container.ReadFrameCollection();

            foreach (var item in container.Frames)
            {
                System.Console.WriteLine("{0} => {1}", item.FrameID, item.FrameData);
            }


            System.Console.WriteLine("fertig ... warte");
            System.Console.ReadKey();


        }
    }
}
