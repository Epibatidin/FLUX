using System.Collections.Generic;
using System.Web.Mvc;
using MP3Renamer.DataContainer.EntityInterfaces;

namespace MP3Renamer.FileIO
{
    public class FileManager
    {
        //-----------------------------------------------------------------------------------------------------------------------
        private FileManager() { }
        public static readonly FileManager Get = new FileManager();
        public static void Refresh(string FilePath)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            FileLoader FL = new FileLoader(FilePath);
            Get.RootCount = 0;
            Get.ResetPos();
            if (FL.FileList != null)
            {            
                var rootList = new StructureFiles(FL).Roots;
                
                var Dict = new Dictionary<int, IRoot>();
                var selItems = new List<byte>();

                if (rootList != null)
                {
                    for (int i = 0; i < rootList.Count; i++)
                    {
                        Dict.Add(i, rootList[i]);
                        selItems.Add((byte)i);
                    }
                }
                Get.roots = Dict;
                Get.SelectedRoots = selItems;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------
        private List<byte> selRoots = null;
        public List<byte> SelectedRoots
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                if (selRoots == null)
                {
                    selRoots = new List<byte>();
                }
                return selRoots;
            }
            set
            {
                selRoots = value;
                updateCounts();
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        public int RootCount { get; private set; }
        public int SubRootCount { get; private set; }
        public int LeafCount { get; private set; }
        private void updateCounts()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            int rootCount = 0;
            int subRootCount = 0;
            int leafCount = 0;

            foreach (var sel in SelectedRoots)
            {
                if (roots.ContainsKey(sel))
                {
                    rootCount++;
                    subRootCount += roots[sel].SubRoots.Count;

                    foreach (var subroot in roots[sel].SubRoots)
                    {
                        leafCount += subroot.Leafs.Count;
                    }
                }
            }
            RootCount = rootCount;
            SubRootCount = subRootCount;
            LeafCount = leafCount;
        }



        //-----------------------------------------------------------------------------------------------------------------------
        public MultiSelectList RootAsMSL
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                return new MultiSelectList(roots, "Key", "Value.Name", SelectedRoots);             
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private Dictionary<int,IRoot> roots = null;
        public IEnumerable<IRoot> Roots()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            foreach (int selectedRoot in SelectedRoots)
            {
                if (roots.ContainsKey(selectedRoot))
                {
                    yield return roots[selectedRoot];
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------
        private int CurrentRoot = 0;
        public IRoot Current
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                int id;
                if (CheckPos(out id))
                    return roots[id];
                else
                    return null;
            }
            set
            {
                int id;
                if (CheckPos(out id))
                    roots[id] = value;
            }
        }



        //-----------------------------------------------------------------------------------------------------------------------
        public bool Next()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            CurrentRoot++;
            if (selRoots != null && selRoots.Count > CurrentRoot)
            {
                return true;
            }
            return false;
        }



        //-----------------------------------------------------------------------------------------------------------------------
        internal void ResetPos()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            CurrentRoot = 0;
        }



        //-----------------------------------------------------------------------------------------------------------------------
        private bool CheckPos(out int id)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            id = -1;
            if (selRoots != null && selRoots.Count > CurrentRoot)
            {
                id = selRoots[CurrentRoot];
                if (roots.ContainsKey(id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}