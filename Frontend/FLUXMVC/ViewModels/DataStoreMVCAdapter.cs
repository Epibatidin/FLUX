using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbstractDataExtraction;
using Tree;
using Interfaces;

namespace FLUXMVC.ViewModels
{
    public class DataStoreMVCAdapter
    {
        public TreeItem<MultiLayerDataContainer> LayerData;

        public DataStoreMVCAdapter(DataStore store)
        {             
            // join alle 
            List<Dictionary<int,ISong>> enums = new List<Dictionary<int,ISong>>();
            List<ITreeItem<ISong>> trees = new List<ITreeItem<ISong>>();

            foreach (var item in store.Iterate().Select(c => c.Data))
            {
                if (item is ITreeItem<ISong>)
                    trees.Add(item as ITreeItem<ISong>);
                else if (item is Dictionary<int, ISong>)
                    enums.Add(item as Dictionary<int, ISong>);                
            }

            LayerData = recursivlyMergeData(trees[0], enums);
            var k = LayerData;
        }

        private TreeItem<MultiLayerDataContainer> recursivlyMergeData(ITreeItem<ISong> tree, List<Dictionary<int, ISong>> layers)
        {   
            var result = new TreeItem<MultiLayerDataContainer>();

            var multi = new MultiLayerDataContainer();
            
            multi.AddSong(tree.Level ,tree.Value);
            foreach (var item in layers)
            {
                if (item.ContainsKey(tree.Value.ID))
                {
                    multi.AddSong(tree.Level, item[tree.Value.ID]);
                }
            }

            result.Level = tree.Level;
            result.Value = multi;

            if (!tree.HasChildren) return result;

            foreach (var item in tree.Children)
            {
                var child = recursivlyMergeData(item, layers);
                if (child != null)
                    result.Add(child);        
            }
            return result;
        }
    }
}