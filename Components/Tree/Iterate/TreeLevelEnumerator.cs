using System.Collections.Generic;

namespace Tree.Iterate
{
    public class TreeLevelEnumerator<T> : AbstractTreeEnumerator<T>
    {       
        private int Level;
        private Dictionary<int, int> LastPoses;
        bool UseRealAlg;

        public TreeLevelEnumerator(TreeItem<T> source, int StartLevel) : base(source)
        {
            // ich will den algo unten nicht ändern 
            // ich will auch keine schalter var einbauen 
            // es muss doch möglich sein 
            Level = StartLevel - 1;
            UseRealAlg = Root.Level != Level;
            if(UseRealAlg)
                LastPoses = new Dictionary<int, int>();

        }

        public override void Dispose()
        {
            LastPoses = null;
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

        bool firstDone;
        private bool FakeIterateIfFound(TreeItem<T> CurNode)
        {
            if (firstDone)
                return false;
            else
            {
                current = Root;
                firstDone = true;
                return true;
            }
        }


        private bool IterateDeepMemPath(TreeItem<T> CurNode)
        {
            if (CurNode.Level == Level) // gewonnen am ziel 
            {
                current = CurNode;
            }
            else // sonst tiefer 
            {
                int pos = 0;
                if (LastPoses.ContainsKey(CurNode.Level)) // wenn ich an diesem lvl schoneinmal war 
                {
                    // next sibling bei der nächsten iteration 
                    pos = LastPoses[CurNode.Level];
                    pos += 1;
                    LastPoses[CurNode.Level] = pos;
                }
                else // sonst 
                    LastPoses.Add(CurNode.Level, pos);  // merk dir die aktuelle position (0)

                if (pos < CurNode.Count) // wenn es an dieser position kinder gibt 
                    return IterateDeepMemPath(CurNode[pos]); // 1 tiefer
                else
                    LastPoses.Remove(CurNode.Level); // sonst bin ich mit dem level fertig
            }
            return LastPoses.Count != 0; // solange es noch pfade gibt 
        }

        public override bool MoveNext()
        {
            if (UseRealAlg)
                return IterateDeepMemPath(Root);
            else
                return FakeIterateIfFound(Root);
        }

        public override void Reset()
        {
            if (UseRealAlg)
                LastPoses.Clear();
            else
                firstDone = false;
        }

    }
}
