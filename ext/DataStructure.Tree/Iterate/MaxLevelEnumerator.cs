using System.Collections.Generic;

namespace DataStructure.Tree.Iterate
{
    public class MaxLevelEnumerator<T> : AbstractTreeEnumerator<T>
    {
        PathEnumerator<T> _iter;
        readonly int _distance;
        readonly bool _fromButtom;
        List<int[]> _pathes;
        int _curPos;


        public MaxLevelEnumerator(TreeItem<T> root) : this(root, 0, false) {}
        public MaxLevelEnumerator(TreeItem<T> root, int distance, bool fromRootToLeaf) : base(root)
        {
            _distance= distance;
            _fromButtom = fromRootToLeaf;
            Reset();
        }
        


        // ziel ist es immer 1 oder 4 level unter den end blättern zu bleiben         
        public override bool MoveNext()
        {
            _curPos++;
            var next = _curPos < _pathes.Count;
            if(next)
                current = _iter.NavigateToItem(_pathes[_curPos]);

            return next;
        }

        /*
         * ich hab nen baum der maximalen tiefe 21 DH das tiefste blatt hat 21 (=MaxDepth) als level wert 
         * ich möchte ab dem Root mit 14 (= Root.Level) mit einer entfernung von 3(=Dist) vom jeweils tiefsten blatt 
         * durch den baum iterieren. 
         * DH ich iteriere über alle Knoten der Tiefe 21 - 3 = 18 vom root aus 
         * DH ich benötige eine maximale PfadLänge von 4 = 18 - 14 
         * Mein zulanger Pfad ist 5(=Path.Count) element lang (von 14 zu 21 beim erstem kind begonnen) 
         * Ich benötige aber nur einen pfad von 14 zu allen der tiefe 18 = 3 
         * 
         * maxpathlength = MaxDepth - Dist - Root.Level - 1;
         * MaxDepth = Root.Level + Path.Count + 1;
         * 
         * maxpathlength = Root.Level + Path.Count +1 - Dist - Root.Level -1 
         *               = Path.Count - Dist 
         * 
         * */
        [System.Diagnostics.DebuggerStepThrough]
        private int[] ShortenPath(List<int> path)
        {
            int length = _fromButtom ? _distance : path.Count - _distance;
            var result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = path[i];
            }
            return result;
        }



        public override void Reset()
        {
            _iter = new PathEnumerator<T>(Root);
            _curPos = -1;
            _pathes = new List<int[]>();

            while (_iter.MoveNext())
            {
                var shortPath = ShortenPath(_iter.Current);
                if (_pathes.Count == 0)
                    _pathes.Add(shortPath);
                else
                    AddPathIfNotExists(shortPath);
            }
        }


        private bool Contains(int[] a)
        {
            for (int i = _pathes.Count - 1; i >= 0; i--)
            {
                // equal reicht nicht es muss all equal geben 
                if (AreEqual(_pathes[i], a))
                    return true;
            }
            return false;
        }


        private void AddPathIfNotExists(int[] shortendPath)
        {
            if (!Contains(shortendPath))
                _pathes.Add(shortendPath);
        }

        private bool AreEqual(int[] a, int[] b)
        {
            if (a.Length != b.Length)
                return false;

            // in meinen Fall ist es wahrscheinlicher 
            // das sich die elemente auf höhere ebene unterscheiden 
            // darum rückwärts
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
    }
}
