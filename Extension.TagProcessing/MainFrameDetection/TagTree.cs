using System;
using System.Collections.Generic;
using System.IO;
using Extension.TagProcessing.TagReader;

namespace Extension.TagProcessing.MainFrameDetection
{
    public class TagValue
    {
        public TagValue() { }

        public TagValue(Func<Stream, bool> detection)
        {
            DetectionFunction = detection;
        }

        public Func<BaseTagReader> Create;       
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


        private static T CreateGeneric<T>() where T : BaseTagReader, new()
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
                                Create = CreateGeneric<ID3V23TagReader> 
                            }
                        },
                        new TagItem()
                        {
                            Value = new TagValue(DetectionMethods.IsID3V3)
                            {
                                Create = CreateGeneric<ID3V23TagReader> 
                            }
                        },                        
                        new TagItem()
                        {
                            Value = new TagValue(DetectionMethods.IsID3V2)
                            {
                                Create = CreateGeneric<ID3V23TagReader> 
                            }
                        }
                    }
                },
                new TagItem()
                {          
                    Value = new TagValue(ReturnTrue)
                    {
                        Create = CreateGeneric<BaseTagReader>
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
