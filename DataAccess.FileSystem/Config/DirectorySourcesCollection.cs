using System.Collections.Generic;
using System.Configuration;
using Extension.Configuration;

namespace DataAccess.FileSystem.Config
{
    [ConfigurationCollection(typeof(FolderSource), AddItemName = "Folder")]
    public class DirectorySourcesCollection : GenericElementCollection<FolderSource>
    {
        [ConfigurationProperty("Root", IsRequired = true)]
        public string Root
        {
            get
            {
                return (string)base["Root"];
            }
        }
        
        //public IVirtualFileProvider Create(string sourceKey)
        //{
        //    DirectoryVirtualFileProvider fp = null;

        //    var item = Item(sourceKey);
        //    if (item != null)
        //    {
        //        fp = new DirectoryVirtualFileProvider();
        //        string path = Path.Combine(Root, item.SubFolder);

        //        fp.Setup(new RealDirectory(new DirectoryInfo(path)));
        //    }
        //    return fp;
        //}

        private List<string> _keys; 
        public IEnumerable<string> Keys
        {
            get 
            {
                if (_keys == null)
                {
                    _keys = new List<string>();
                    for (int i = 0; i < Count; i++)
                    {
                        _keys.Add(Item(i).Key);
                    }
                }
                return _keys;
            }
        }
    }
}
