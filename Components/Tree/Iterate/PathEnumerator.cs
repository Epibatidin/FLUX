using System;
using System.Collections.Generic;

namespace Tree.Iterate
{

    // iteriert über alle pfade in baum von der wurzel zu den blättern 
    // die vererbung ist aber glaub falsch 
    // das resultat ist eine liste von Level : Value Pairs 
    [System.Diagnostics.DebuggerNonUserCode]
    public class PathEnumerator<T> : IEnumerator<List<int>> , IEnumerator<TreeItem<T>>
    {
        List<int> CurrentPath;        
        TreeItem<T> root;
        bool cache = false;

        public PathEnumerator(TreeItem<T> Root)
        {
            root = Root;
            Reset();          
        }

        
        public bool MoveNext()
        {
            var result = cache;
            cache = FindNextNode(root);

            return !result;
        }


        // DO NOT TOUCH !!1einself
        private bool FindNextNode(TreeItem<T> source)
        {
            if (source.HasChildren)
            {
                int pos = GetPositionByLevel(source.Level);
                if (pos < 0)
                {
                    // in diesem Level war ich noch nicht 
                    // ich beginne also bei 0
                    pos = 0;
                    SetPos(source.Level, pos);
                }
                
                // kontrolliere ob ich tiefer gehen kann 
                if (pos < source.Count)
                {
                    // ja geh tiefer 
                    var EndFound = FindNextNode(source[pos]);
                    // kann ich auch weiter gehen ? 

                    // das ende eines pfades weil keine weiteren kinder gefunden  = true 
                    if (EndFound)
                    {
                        // gibt es ein nächstes kind ? 
                        EndFound = !((pos + 1) < source.Count);
                        if (EndFound) // nein ich kann nicht zur seite 
                        {
                            // dann muss ich solange weiter nach oben gehen (richtung wurzel) bis ich zur seite kann                            
                            RemoveLast();                                             
                        }
                        else // ja ich kann zur seite 
                        {
                            IncLevel(source.Level);
                        }
                    }
                    return EndFound;
                }
                else // sonst kann ich auch nicht zur seite gehen
                {
                    return true;
                }
            }
            else
            {
                Current = new List<int>(CurrentPath);
                CurrentItem = source;
                return true;
            }
        }

        public TreeItem<T> CurrentItem { get; private set; }
        
        public TreeItem<T> NavigateToItem(IEnumerable<int> Path)
        {
            var cur = root;
            foreach (var item in Path)
            {
                cur = cur[item];                
            }
            return cur;
        }
    
              
        [System.Diagnostics.DebuggerStepThrough]
        private int GetPositionByLevel(int NonMappedLevel)
        {
            var listpos = NonMappedLevel - root.Level;
            if (CurrentPath.Count > listpos)
                return CurrentPath[listpos];
            else
                return -1;
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void SetPos(int NonMappedLevel, int Pos)
        {
            var listpos = NonMappedLevel - root.Level;
            if (CurrentPath.Count > listpos)
                CurrentPath[listpos] = Pos;
            else
                CurrentPath.Add(Pos);
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void IncLevel(int NonMappedLevel)
        {
            var listpos = NonMappedLevel - root.Level;
            if (CurrentPath.Count > listpos)
                CurrentPath[listpos]++; 
            else 
                throw new Exception();
        }

        private void RemoveLast()
        {
            CurrentPath.RemoveAt(CurrentPath.Count -1);
        }
   
        public void Reset()
        {
            CurrentPath = new List<int>();
            cache = false;
            Current = null;
        }


        object System.Collections.IEnumerator.Current { get { return Current; } }
        public List<int> Current { get; private set; }
        TreeItem<T> IEnumerator<TreeItem<T>>.Current { get { return CurrentItem; } }

        public void Dispose()
        {
            
        }

        
             
    }
}
