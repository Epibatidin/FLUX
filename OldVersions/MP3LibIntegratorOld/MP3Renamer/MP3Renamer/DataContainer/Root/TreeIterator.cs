using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP3Renamer.FileIO;
using MP3Renamer.DataContainer.EntityInterfaces;

namespace MP3Renamer.DataContainer.Root
{
    public class TreeIterator
    {
        public IEnumerable<IteratorContainer> IterateInWriteFileOrder()
        {
            FileManager.Get.ResetPos();
            // ich brauch eine methode in dem aktuellem subknoten auf den letzten zurück zu gehen 
            // also wenn alle leafs in subroot(S1) fertig ist zurück zu diesem subroot(S1)

            foreach (var item in FileManager.Get.Roots())
            {
                foreach (var root in HandleRoot(item))
                {
                    yield return root;
                }
            }                        
            // wie sieht der plan aus ? 
            // ich muss in einer reihenfolge iteriren 
            // dabei muss ich erst jede stufe fertig machen bevor ich zur nächsten gehe 
            // dazu muss die letzte stufe auf den stack 
            // also kommt erst der ganze root auf den stack   
            
        }

        private IEnumerable<IteratorContainer> HandleRoot(IRoot root)
        {
            var subroots = root.SubRoots;
                       
            var container = new IteratorContainer();
            
            container.SetData(root);
            container.Stop = true;
            foreach (var item in subroots)
            {
                container.SetData(item);
                container.Stop = true;
                foreach (var leaf in item.Leafs)
                {
                    container.SetData(leaf);
                    yield return container;
                    container.Stop = false;
                }                
            }
        }
    }
}