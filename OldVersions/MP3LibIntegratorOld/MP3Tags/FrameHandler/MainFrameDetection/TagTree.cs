using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Tree;
using System.IO;

namespace FrameHandler.MainFrameDetection
{
    using TagItem = TreeItem<TagValue>;
    using FrameHandler.ContainerDefs;
    public class TagValue
    {
        public TagValue() { }

        public TagValue(Func<Stream, bool> detection)
        {
            DetectionFunction = detection;
        }

        public Func<Container> Create;       
        public Func<Stream,bool> DetectionFunction;        
    }

    
    public class TagTree
    {
        private static TagTree Instance;

        private TagTree()
        {
            Create();
        }

        private TagItem Root;


        private static T CreateGeneric<T>() where T : Container, new()
        {
            return new T();
        }

        public static TagItem Get
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new TagTree();
                }
                return Instance.Root;
            }
        }

        private void Create()
        {
            Root = new TagItem();
            Root.Value = new TagValue(ReturnTrue);
            Root.Children = new List<TagItem>()
            {
                new TagItem()
                {
                    Value = new TagValue(DetectionMethods.IsID3),
                    Children = new List<TagItem>()
                    {                        
                        new TagItem()
                        {
                            Value = new TagValue(DetectionMethods.IsID3V4)
                            {
                                Create = CreateGeneric<ID3V23> 
                            }
                        },
                        new TagItem()
                        {
                            Value = new TagValue(DetectionMethods.IsID3V3)
                            {
                                Create = CreateGeneric<ID3V23> 
                            }
                        }
                    }
                },
                new TagItem()
                {
                    Value = new TagValue(ReturnTrue)
                    {
                        Create = CreateGeneric<Container>
                    }
                    
                }
            };

        }

        private static bool ReturnTrue(Stream stream)
        {
            return true;
        }


    }
}
