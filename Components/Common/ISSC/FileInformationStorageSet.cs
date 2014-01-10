using System.IO;

namespace Common.ISSC
{
    [System.Diagnostics.DebuggerStepThrough]
    public class FileInformationStorageSet : InformationStorageSet
    {
        public FileInformationStorageSet(FileInfo info)
        {
            setByKey(FIIS.FileInfo, info);
            setByKey(FIIS.FileName, getFileNameWithoutExtension(info));
            setByKey(FIIS.Extension, info.Extension.Remove(0, 1));            
        }

        private string getFileNameWithoutExtension(FileInfo fi)
        {
            var dotpos = fi.Name.LastIndexOf(fi.Extension);
            if (dotpos >= 0)
                return fi.Name.Remove(dotpos);
            else
                return fi.Name;
        }


        public FileInformationStorageSet(DirectoryInfo di)
        {
            setByKey(FIIS.DirectoryPart, di);
        }

        public T getByKey<T>(FIIS Key)
        {
            return base.getByKey<T>((int)Key);
        }

        public void setByKey<T>(FIIS key, T value)
        {
            base.setByKey<T>((int)key, value);
        }
    }
}
