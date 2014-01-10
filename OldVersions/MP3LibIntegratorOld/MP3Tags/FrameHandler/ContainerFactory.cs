using System;
using System.Collections.Generic;
using System.IO;
using FrameHandler.ContainerDefs;
using Helper;
using FrameHandler.MainFrameDetection;
using FrameHandler.Frames;
using Common.Tree;

namespace FrameHandler
{

    using TagItem = TreeItem<TagValue>;
    public class ContainerFactory
    {
        public ContainerFactory()
        {
        }
        
        private Stream CurrentStream;

        public Container Create(Stream source)
        {
            CurrentStream = source;

            var facMethod = getTypeFromTree(TagTree.Get);
            if (facMethod == null)
                throw new Exception("Codec not Suporrted");

            var container = facMethod();
            container.SetSourceStream(source);

            return container;
        } 


        private Func<Container> getTypeFromTree(TagItem Root)
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



        private Func<Container> getTypeFromTree2(TagItem Root)
        {
            // wenn eine detections methode erfolgreich war 
            if (Root.Value.DetectionFunction(CurrentStream))
            {
                // muss ich im unterbraum weiter suchen
                // wenn er kinder hat 
                if (Root.Children == null)
                {
                    // dann muss ich sie durch iterieren 
                    foreach (TagItem child in Root.Children)
                    {
                        var childResult = getTypeFromTree2(child);
                        if (childResult != null)
                            return childResult;
                    }
                    return null;
                }
                else // sonst 
                {
                    // bin ich fertig und muss den erzeuger zurück geben 
                    return Root.Value.Create;
                }
            }
            else // sonst
            {
                // muss ich in den "siblings" weiter suchen 
                // das kann ich hier aber nicht machen 
                // darum muss ich die ausführung hier abbrechen 
                return null;
            }
        }
    }
}
