using System.Collections.Generic;
using System.IO;
using System.Linq;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.DataContainer.Music;
using MP3Renamer.Models.DataContainer.Music;


namespace MP3Renamer.FileIO
{
    public class StructureFiles
    {
        FileInfo[] unstructetFiles = null;
        string InitalPath;
        
        public StructureFiles(FileLoader fileLoader)
        {
            unstructetFiles = fileLoader.FileList.ToArray();
            InitalPath = fileLoader.InitalPath;
        }


        private List<IRoot> roots = null;
        public List<IRoot> Roots
        {
            get
            {
                if (roots == null)
                {
                    roots = ReOrderFiles();
                }
                return roots;
            }
            set
            {
                roots = value;
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private List<IRoot> ReOrderFiles()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            List<IRoot> roots = new List<IRoot>();

            string memRoot = "";
            string memSubRoot = "";


            IRoot root = null;
            ISubRoot subroot = null;
            ILeaf leaf = null;
            List<ILeaf> leafs = new List<ILeaf>();

            if (unstructetFiles.Count() == 0) return null;

            for (int i = 0; i < unstructetFiles.Count(); i++)
            {
                IDataContainer IDC = createContainer(unstructetFiles[i]);

                if (IDC == null) continue;

                if (memSubRoot != IDC.SubRootAsString)
                {
                    memSubRoot = IDC.SubRootAsString;
                    if (root != null && subroot != null)
                    {
                        subroot.Leafs = leafs;
                        leafs = new List<ILeaf>();
                        root.SubRoots.Add(subroot);
                    }
                    subroot = new Album(IDC.SubRootAsString);
                }

                if (memRoot != IDC.RootAsString)
                {
                    // 
                    memRoot = IDC.RootAsString;

                    if (root != null)
                        roots.Add(root);
                    root = new Artist(IDC.RootAsString);
                }
                leaf = new Song(IDC.LeafAsString, unstructetFiles[i].FullName);
                leafs.Add(leaf);
            }


            subroot.Leafs = leafs;
            root.SubRoots.Add(subroot);
            roots.Add(root);

            return roots;            
        }



        //-----------------------------------------------------------------------------------------------------------------------
        private IDataContainer createContainer(FileInfo fileInfo)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            // ich brauhc jmd der mir instancen baut 
            // das hat aber nix mit dem fileloader zu tun 
            // structure files muss das tun 
            
            FileType fType = ExtensionHelper.getTypeOfExtension(fileInfo.Extension);

            switch (fType)
            {
                case FileType.Music:
                    return new MusicDataContainer(fileInfo, InitalPath);
                case FileType.Video:
                    break;
                case FileType.Picture:
                    break;
            }
            return null;
        }
    }
}