using System.Collections.Generic;

namespace DataStructure.Tree.Iterate
{
    public class TreeIterator
    {
        public static IEnumerable<T> IterateDepth<T>(TreeItem<T> source) 
        {
            yield return source.Value;

            if (source.HasChildren)
            {
                foreach (TreeItem<T> child in source.Children)
                {
                    foreach (var item in IterateDepth(child))
                    {
                        yield return item;
                    }                    
                }
            }
        }

        public static IEnumerable<TreeItem<T>> IterateDepthGetTreeItems<T>(TreeItem<T> source)
        {
            yield return source;

            if (source.HasChildren)
            {
                foreach (TreeItem<T> child in source.Children)
                {
                    foreach (var item in IterateDepthGetTreeItems(child))
                    {
                        yield return item;
                    }
                }
            }
        }

      
        //public static IEnumerable<T> GetItemsOfLevel<T>(TreeItem<T> source, int Level)
        //{
        //    if (source.Level == Level) // dann bin ich eigentlich zu weit weil ich wollt ja alle von dieser ebene 
        //    {
        //        yield return source.Value;
        //        yield break;
        //    }
        //    else if(source.Level < Level) // wenn ich noch ne ebene drunter bin ist alles noch okay 
        //    {
        //        if(source.HasChildren)
        //        {                    
        //            if(source.Level == Level -1 )
        //            {

        //            }
        //            else // geh ne strufe tiefer  -- geht aber irgendwie nich ... - viele items und so
        //            {
                    
        //            }
        //        }
        //    }
        //}

        //public IEnumerable<IteratorContainer> IterateInWriteFileOrder()
        //{
        //    FileManager.Get.ResetPos();
        //     ich brauch eine methode in dem aktuellem subknoten auf den letzten zurück zu gehen 
        //     also wenn alle leafs in subroot(S1) fertig ist zurück zu diesem subroot(S1)

        //    foreach (var item in FileManager.Get.Roots())
        //    {
        //        foreach (var root in HandleRoot(item))
        //        {
        //            yield return root;
        //        }
        //    }
        //     wie sieht der plan aus ? 
        //     ich muss in einer reihenfolge iteriren 
        //     dabei muss ich erst jede stufe fertig machen bevor ich zur nächsten gehe 
        //     dazu muss die letzte stufe auf den stack 
        //     also kommt erst der ganze root auf den stack   

        //}

        //private IEnumerable<IteratorContainer> HandleRoot(IRoot root)
        //{
        //    var subroots = root.SubRoots;
                       
        //    var container = new IteratorContainer();
            
        //    container.SetData(root);
        //    container.Stop = true;
        //    foreach (var item in subroots)
        //    {
        //        container.SetData(item);
        //        container.Stop = true;
        //        foreach (var leaf in item.Leafs)
        //        {
        //            container.SetData(leaf);
        //            yield return container;
        //            container.Stop = false;
        //        }                
        //    }
        //}
    }
}