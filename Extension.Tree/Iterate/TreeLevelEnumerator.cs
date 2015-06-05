using System.Collections.Generic;

namespace Extension.Tree.Iterate
{
    public class TreeLevelEnumerator<T> : AbstractTreeEnumerator<T>
    {       
        private readonly int _level;
        private Dictionary<int, int> _lastPoses;
        readonly bool _useRealAlg;
        bool _firstDone;

        public TreeLevelEnumerator(TreeItem<T> source, int startLevel) : base(source)
        {
            // ich will den algo unten nicht ändern 
            // ich will auch keine schalter var einbauen 
            // es muss doch möglich sein 
            _level = startLevel - 1;
            _useRealAlg = Root.Level != _level;
            if(_useRealAlg)
                _lastPoses = new Dictionary<int, int>();

        }

        public override void Dispose()
        {
            _lastPoses = null;
        }

        // wenn ich alle items von level 2 haben will 
        // returne ich eine enum von level 1 nodes 
        // dh wenn das gewünschte level -1 == den aktuellem ist bin ich fertig

        // ich brauch eigentlich keinen stack sondern ne liste 
        // die die aktuell letzten positionen festhält 

        // ich brauch ne abbruch bedinung 
        // die abbruch bedinung ist wenn current = StartNode ist 
        // das geht nicht weil dann auch gleich beim ersten mal raus bin 
        // ich brauch quasi ne bedingung für den 2ten durchlauf 
        // was nich geht 

       
        private bool FakeIterateIfFound(TreeItem<T> curNode)
        {
            if (_firstDone)
                return false;
            
            current = Root;
            _firstDone = true;
            return true;
        }


        private bool IterateDeepMemPath(TreeItem<T> curNode)
        {
            if (curNode.Level == _level) // gewonnen am ziel 
            {
                current = curNode;
            }
            else // sonst tiefer 
            {
                int pos = 0;
                if (_lastPoses.ContainsKey(curNode.Level)) // wenn ich an diesem lvl schoneinmal war 
                {
                    // next sibling bei der nächsten iteration 
                    pos = _lastPoses[curNode.Level];
                    pos += 1;
                    _lastPoses[curNode.Level] = pos;
                }
                else // sonst 
                    _lastPoses.Add(curNode.Level, pos);  // merk dir die aktuelle position (0)

                if (pos < curNode.Count) // wenn es an dieser position kinder gibt 
                    return IterateDeepMemPath(curNode[pos]); // 1 tiefer
                else
                    _lastPoses.Remove(curNode.Level); // sonst bin ich mit dem level fertig
            }
            return _lastPoses.Count != 0; // solange es noch pfade gibt 
        }

        public override bool MoveNext()
        {
            if (_useRealAlg)
                return IterateDeepMemPath(Root);
            else
                return FakeIterateIfFound(Root);
        }

        public override void Reset()
        {
            if (_useRealAlg)
                _lastPoses.Clear();
            else
                _firstDone = false;
        }
    }
}
