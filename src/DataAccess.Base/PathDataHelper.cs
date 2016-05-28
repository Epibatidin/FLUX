namespace DataAccess.Base
{
    public class PathDataHelper
    {
        public PathData FullPathToVirtualPathData(string absoluteFilePath, string rootDirPath)
        {
            var pathData = new PathData();

            var extPos = absoluteFilePath.LastIndexOf('.');
            if (extPos == -1)
                extPos = absoluteFilePath.Length;

            pathData.Extension = absoluteFilePath.Substring(extPos);

            var pureVirtualFilePath = absoluteFilePath.Substring(rootDirPath.Length, absoluteFilePath.Length - extPos);
            pathData.PathParts = pureVirtualFilePath.Split('\\');

            return pathData;
        }


    }
}
