using System.Collections.Generic;

namespace MP3Renamer.DataContainer.EntityInterfaces
{
    public interface IRoot
    {
        byte ID { get; set; }

        // der artisten name
        string Name { get; }
                
        List<ISubRoot> SubRoots { get; set; }



    }
}
