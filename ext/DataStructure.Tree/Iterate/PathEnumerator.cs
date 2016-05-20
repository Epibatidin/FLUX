using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Iterate
{
    // iteriert über alle pfade in baum von der wurzel zu den blättern 
    // die vererbung ist aber glaub falsch 
    // das resultat ist eine liste von Level : Value Pairs 
    public class PathEnumerator<T> : IEnumerator<List<int>> , IEnumerator<TreeItem<T>>
    {
        List<int> _currentPath;
        readonly TreeItem<T> _root;
        bool _cache = false;

        public PathEnumerator(TreeItem<T> root)
        {
            _root = root;
            Reset();          
        }

        
        public bool MoveNext()
        {
            var result = _cache;
            _cache = FindNextNode(_root);

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
                    var endFound = FindNextNode(source[pos]);
                    // kann ich auch weiter gehen ? 

                    // das ende eines pfades weil keine weiteren kinder gefunden  = true 
                    if (endFound)
                    {
                        // gibt es ein nächstes kind ? 
                        endFound = !((pos + 1) < source.Count);
                        if (endFound) // nein ich kann nicht zur seite 
                        {
                            // dann muss ich solange weiter nach oben gehen (richtung wurzel) bis ich zur seite kann                            
                            RemoveLast();                                             
                        }
                        else // ja ich kann zur seite 
                        {
                            IncLevel(source.Level);
                        }
                    }
                    return endFound;
                }
                else // sonst kann ich auch nicht zur seite gehen
                {
                    return true;
                }
            }
            else
            {
                CurrentPath = new List<int>(_currentPath);
                CurrentItem = source;
                return true;
            }
        }

        public TreeItem<T> CurrentItem { get; private set; }
        public List<int> CurrentPath { get; private set; }
        public TreeItem<T> NavigateToItem(IEnumerable<int> path)
        {
            var cur = _root;
            foreach (var item in path)
            {
                cur = cur[item];                
            }
            return cur;
        }
          
        private int GetPositionByLevel(int nonMappedLevel)
        {
            var listpos = nonMappedLevel - _root.Level;
            if (_currentPath.Count > listpos)
                return _currentPath[listpos];
            else
                return -1;
        }
        
        private void SetPos(int nonMappedLevel, int pos)
        {
            var listpos = nonMappedLevel - _root.Level;
            if (_currentPath.Count > listpos)
                _currentPath[listpos] = pos;
            else
                _currentPath.Add(pos);
        }
        
        private void IncLevel(int nonMappedLevel)
        {
            var listpos = nonMappedLevel - _root.Level;
            if (_currentPath.Count > listpos)
                _currentPath[listpos]++; 
            else 
                throw new Exception();
        }

        private void RemoveLast()
        {
            _currentPath.RemoveAt(_currentPath.Count -1);
        }
   
        public void Reset()
        {
            _currentPath = new List<int>();
            _cache = false;
            CurrentPath = null;
        }

        public List<int> Current => CurrentPath;
        object System.Collections.IEnumerator.Current => Current;
        TreeItem<T> IEnumerator<TreeItem<T>>.Current => CurrentItem;

        public void Dispose()
        {
            
        }

        
             
    }
}
