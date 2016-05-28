using System;

namespace DataAccess.Base
{
    public class PathDataHelper
    {
        public PathData FullPathToVirtualPathData(string absoluteFilePath, string rootDirPath)
        {
            var pathData = new PathData();

            var extPos = absoluteFilePath.LastIndexOf('.');
            if (extPos == -1)
            {
                extPos = absoluteFilePath.Length + 1;
                pathData.Extension = String.Empty;
            }
            else
                pathData.Extension = absoluteFilePath.Substring(++extPos);
            
            var pureVirtualFilePath = absoluteFilePath.Substring(rootDirPath.Length +1 ,extPos- rootDirPath.Length -2);
            pathData.PathParts = pureVirtualFilePath.Split('\\');

            return pathData;
        }


    }
}
