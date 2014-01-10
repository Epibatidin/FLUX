using System.IO;
using System.Linq;
using FrameHandler;
using InFileDataProzessor.TagWriter;
using MockUp;
using MockUp.XMLItems;

namespace MockExecute
{
    class Program
    {
        static void Main(string[] args)
        {
            serialize();
            //createFakeStream(@"E:\FluxWorkBenchFiles\ComponentTests\Working\1.mp3");
        }

        private static void blub()
        {
            var source = createTestObject("SingleDataDummy");
            //var item = source.Groups[0].Items[0];

            ContainerFactory fac = new ContainerFactory();
            //var data = fac.Create(item.Open(), true);

        }

        private static string getFileName(string fileName)
        {
            return string.Format(@"E:\FluxWorkBenchFiles\XML\{0}.xml", fileName);
        }

        private static Source createTestObject(string fileName)
        {
            var file = new FileStream(getFileName(fileName), FileMode.Open, FileAccess.Read);
            return null;
            //return Root.DeSerialize(file).Source;
        }


        private static void createFakeStream(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            ContainerFactory fac = new ContainerFactory();
            var data = fac.Create(fs);

            MemoryStream s = new MemoryStream();

            ID3V23TagWriter writer = new ID3V23TagWriter(s);

            writer.WriteFrame(data.Frames);

            s.Seek(0, SeekOrigin.Begin);

            data = fac.Create(s);
            var l = data;
        }

        private static void serialize()
        {
            XMLCreator c = new XMLCreator();

            c.SerializeFolder("Amduscia");
        }
    }
}
