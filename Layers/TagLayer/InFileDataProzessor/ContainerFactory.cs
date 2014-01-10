using System;
using System.IO;
using FrameHandler.MainFrameDetection;

namespace FrameHandler
{

    using TagItem = Tree.EasyTreeItem<TagValue>;
    using InFileDataProzessor.TagReader;
    using System.Collections.Generic;
    using InFileDataProzessor;

    public class ContainerFactory
    {
        bool _ignorePrivateData = true;
        public ContainerFactory(bool ignorePrivateData = true)
        {
            _ignorePrivateData = ignorePrivateData;
        }
        
        private Stream CurrentStream;

        public MP3Data Create(Stream source)
        {
            CurrentStream = source;

            var facMethod = getTypeFromTree(TagTree.Get);
            if (facMethod == null)
                throw new Exception("Codec not Suporrted");

            var container = facMethod();
            container.SetStream(source);

            return container.ReadFrame(_ignorePrivateData);
        } 


        private Func<BaseTagReader> getTypeFromTree(TagItem Root)
        {
            // wenn eine detections methode erfolgreich war 
            if (Root.Value.DetectionFunction(CurrentStream))
            {
                // muss ich im unterbraum weiter suchen
                // wenn er kinder hat 
                if (Root.Children != null)
                {
                    // dann muss ich sie durch iterieren 
                    foreach (TagItem child in Root.Children)
                    {                        
                        var childResult = getTypeFromTree(child);
                        if (childResult != null)
                            return childResult;
                    }
                }
                else // sonst 
                {
                    // bin ich fertig und muss den erzeuger zurück geben 
                    return Root.Value.Create;
                }
            }
            return null;
        }
    }
}
