using System;
using System.Linq;
using System.Collections.Generic;
using Tree.Iterate;

namespace Tree
{
    public class TreeTravers
    {
        // ziel ist es in alle unterbäume der tiefe n 
        // eine ebene einzufügen 
        public TreeItem<T> GrowTreeToLevel<T>(TreeItem<T> Root, int StretchToLevel, Func<T> Fac)
        {
            MaxLevelEnumerator<T> enumer = new MaxLevelEnumerator<T>(Root, 1, false);
            List<TreeItem<T>> memChilds = null;

            while (enumer.MoveNext())
            {
                memChilds = null;
                var cur = enumer.Current;
                if (cur.Level < StretchToLevel)
                {                   
                    if (cur.HasChildren)
                    {
                        memChilds = cur.GetChildren();
                        cur.SetChildren(new List<TreeItem<T>>());
                        foreach (var item in memChilds)
                        {
                            item.Level = StretchToLevel;
                        }
                    }
                    var parent = cur;
                    TreeItem<T> child = null;
                    // füg den neuen leeren baum ein 
                    for (int i = cur.Level + 1; i < StretchToLevel; i++)
                    {
                        child = new TreeItem<T>();
                        child.Value = Fac();
                        child.Level = i;
                        parent.Add(child);
                        parent = child;
                    }
                    parent.SetChildren(memChilds);
                }               
            }
            return Root;
        }


        //public TreeItem<T> GrowTreeToLevel2<T>(TreeItem<T> Root, int StretchToLevel, Func<T> Fac)
        //{
        //    // als erstes muss ich alle unter bäume finden die nicht tief genug sind 
        //    // bis zu den blättern 

        //    var shortestTree = GetShortestSubTreeLevel(Root);
            
        //    var r = new TreeLevelEnumerator<T>(Root, shortestTree);
        //    List<TreeItem<T>> memChilds = null; 
            
        //    while (r.MoveNext())
        //    {
        //        memChilds = null; 
        //        // ich brauch sowhl die kinder als auch den elter 
        //        // ich muss mir beide merken 
        //        // den elter darf ich nicht bewegen sonst bekomm ich ihn nicht mehr in den ursprungsbaum zurück
        //        // ich muss also die kinder in einen neuen knoten VERSCHIEBEN 
        //        // bzw würd ich es doch vorwärts bevorzugen 
        //        // ich nehm also die kinder merk sie mir modifiziere ihr aktuelles level 
        //        // löse die verbindung zum root 
        //        // füge items ein bis ich das level der kinder -1 erreicht hab 

        //        var cur = r.Current;
        //        if (cur.HasChildren)
        //        {
        //            memChilds = cur.Children;
        //            cur.Children = new List<TreeItem<T>>();
        //            foreach (var item in memChilds)
        //            {
        //                item.Level = StretchToLevel;
        //            }
        //        }
        //        var parent = cur;
        //        TreeItem<T> child = null;
        //        // füg den neuen leeren baum ein 
        //        for (int i = cur.Level + 1 ; i < StretchToLevel ; i++)
        //        {
        //            child = new TreeItem<T>();
        //            child.Value = Fac();
        //            child.Level = i;
        //            parent.Add(child);
        //            parent = child;
        //        }
        //        parent.SetChildren(memChilds);
        //    }
        //    return Root;
        //}

        //private int GetShortestSubTreeLevel<T>(TreeItem<T> root)
        //{
        //    return 3;
        //}


        //public TreeItem<T> AddLevelToShortSubTrees<T>(TreeItem<T> Root, int TargetLevel)
        //{
        //    // how to ?=! 

        //    // als erstes muss ich JEDEN Subbaum iterieren .... 
        //    // blätter sind dabei egal 
        //    // ich muss mir den weg merken 
        //    // damit ich bäume nicht doppelt iteriere
        //    // und somit muss ich mir wieder den pfad merken 
        //    // schon gemacht !! nur nen bissl anders 
        //    // also 


        //    if (Root.Level < TargetLevel)
        //    {
        //        // na tiefer ! 
        //        if (Root.SaveCount() > 0)
        //        {
        //            // juhu ich darf tiefer 

        //        }
        //        else
        //        {
        //            // am ziel
        //            //var emptySub = CreateEmptySubTree<T>(Root.Level, TargetLevel);
                    
        //        }
        //    }

        //    return Root;
        //}

        //// es ist sinnlos die erzeugung vom einfügen zu trennen => doppelter aufwand 
        //// => CreateEmptySubTree Muss sterben 

        //public TreeItem<T> InsertEmptyItems<T>(TreeItem<T> Root, int MaxLevel)
        //{
        //    // und wieder einmal muss ich mit rekursion arbeiten 
        //    // also neues item erzeugen 
        //    // alle childs setzen 
        //    // sich selber als einziges Child im vorherigem elter setzen 
        //    // ja irgendwie so 
        //    // solange bis Level == MaxLevel

        //    // als erstes muss ich das level der am weitest obeb stehenden kinder auf ziel level setzten 
        //    // sonst muss ich das für jedes level wiederholen ... was doof wäre 
        //    int currentLevel = MaxLevel;
        //    int RootBaseLevel = Root.Level;




        //    if (Root.Level < MaxLevel - 1)
        //    {
        //        if (Root.HasChildren)
        //        {
        //            // verschieb die kinder 
        //            // aber eigentlich muss ich zum tiefstem kind 
        //            for (int i = 0; i < Root.Count; i++)
        //            {
        //                Root[i].Level = MaxLevel;
        //            }
        //            currentLevel = MaxLevel - 1;

        //        } // eine iteration geschaft 
        //    }
            
        //    Root.Level = currentLevel;
            
        //    TreeItem<T> Dummy = null;
        //    for (int i = currentLevel - 1; i >= RootBaseLevel; i--)
        //    {
        //        // root nehmen und solange höher schieben 
        //        Dummy = new TreeItem<T>();
        //        Dummy.Level = i;
        //        Dummy.Add(Root);
        //        Root = Dummy;
        //    }            
        //    return Dummy;
        //}
    }
}
