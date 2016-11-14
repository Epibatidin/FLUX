using DataStructure.Tree.Iterate;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Extraction.Layer.File.Interfaces;
using System;
using System.Collections.Generic;
using FileTreeItem = DataStructure.Tree.TreeItem<Extraction.Layer.File.FileLayerSongDo>;

namespace Extraction.Layer.File
{
    public class FileStructureDataExtractionLayer : IDataExtractionLayer
    {
        private readonly IEnumerable<IPartedStringOperation> _cleaners;
        private readonly IEnumerable<IFullTreeOperator> _treeOperators;

        private readonly ITreeByKeyAccessorBuilder _byTreeAccessorBuilder;
        
        public FileStructureDataExtractionLayer(IEnumerable<IPartedStringOperation> cleaners,
            IEnumerable<IFullTreeOperator> treeOperators,
            ITreeByKeyAccessorBuilder byTreeAccessorBuilder)
        {
            _cleaners = cleaners;
            _treeOperators = treeOperators;
            _byTreeAccessorBuilder = byTreeAccessorBuilder;
        }

        public void Execute(ExtractionContext store, UpdateObject updateObject)
        {
            var treeAccessor = _byTreeAccessorBuilder.Build(store.SourceValues);
            _byTreeAccessorBuilder.BuildKeyMapping(treeAccessor);

            updateObject.UpdateData(treeAccessor);

            foreach (var cleaner in _cleaners)
            {
                ForeachPartedStringInTree(treeAccessor.Tree, cleaner.Operate);
            }

            foreach (var treeOperator in _treeOperators)
            {
                treeOperator.Operate(treeAccessor);
            }
        }

        private void ForeachPartedStringInTree(FileTreeItem root, Action<PartedString> func)
        {
            foreach (var item in TreeIterator.IterateDepthGetTreeItems(root))
            {
                func(item.Value.LevelValue);
            }
        }

        //private void ForeachLvl(FileTreeItem root)
        //{
        //    var lvlIterator = new TreeLevelEnumerator<FileLayerSongDo>(root, 0);
        //    var iter = new MaxLevelEnumerator<FileLayerSongDo>(root, 1, true); // 1 == Album

        //    while(iter.MoveNext())
        //    {
        //        yield return iter.Current.Children;
        //    }



        //}

        //private IEnumerable<List<TreeItem<ISSC>>> IterateBlock(WorkUnit wu)
        //        //{
        //        //    MaxLevelEnumerator<ISSC> iter = new MaxLevelEnumerator<ISSC>(wu.Data, 1, true);
        //        //    yield return wu.Data.Children;

        //        //    while (iter.MoveNext())
        //        //    {
        //        //        yield return iter.Current.Children;
        //        //    }
        //        //    iter = new MaxLevelEnumerator<ISSC>(wu.Data, 2, true);

        //        //    while (iter.MoveNext())
        //        //    {
        //        //        yield return iter.Current.Children;
        //        //    }
        //        //    yield break;
        //        //}

        //{
        //    EqualityCleaner cleaner = new EqualityCleaner(_config.WhiteListConfig);




        //    List<PartedString> currentBlockData = null;
        //    foreach (var block in IterateBlock(W))
        //    {
        //        currentBlockData = new List<PartedString>();
        //        foreach (var treeItem in block)
        //        {
        //            treeItem.Value.Status = Common.DataStatus.Updated;
        //            currentBlockData.Add(treeItem.Value.CleanIIS.getByKey<PartedString>(treeItem.Level));
        //        }
        //        cleaner.Operate(currentBlockData);
        //        var z = currentBlockData;
        //    }
        //    return W;
        //}

        //        private void ExtractYear()
        //        {
        //            YearExtractor exe = new YearExtractor();
        //            foreach (var item in _data.Children) // iteriere alle alben 
        //            {
        //                exe.Execute(item.Value);
        //            }
        //        }


        //        private void ExtractTrack()
        //        {
        //            // iteriere im block auf level 4 und extrahiere tracknr und eventuell cd 
        //            MaxLevelEnumerator<FileLayerSongDo> iter = new MaxLevelEnumerator<FileLayerSongDo>(_data,2, true);

        //            TrackExtractor extractor = new TrackExtractor(c =>
        //            {
        //                return c.Value.LevelValue;
        //            });

        //            while (iter.MoveNext())
        //            {
        //                extractor.CurrentData(iter.Current.GetChildren());
        //                extractor.Execute();
        //            }

        //            // kontrolle ob cds hinzugekommen sind 

        //            // wenn cd - dann restructure tree
        //        }



        //        

        //        //private WorkUnit OneStepUp(WorkUnit W)
        //        //{
        //        //    OneStepUpCleaner cleaner = new OneStepUpCleaner();

        //        //    cleaner.Operate(W.Data);

        //        //    return W;
        //        //}


        //        //private WorkUnit SetUpdated(WorkUnit W)
        //        //{
        //        //    foreach (var item in TreeIterator.IterateDepthGetTreeItems(W.Data))
        //        //    {
        //        //        item.Value.Status = Common.DataStatus.Updated;
        //        //    }
        //        //    return W;
        //        //}

        //        //private WorkUnit endreingung(WorkUnit Wu)
        //        //{
        //        //    /* dann bleiben trotzdem artefakte über wie '()' , '[]' .... 
        //        //     * nach bereinigen 
        //        //     * aber nur wenn inet gefunden wurde ? 
        //        //     * vlt am ende der extraktion ? 
        //        //     * als allgemeine bereingung ? 
        //        //     * alle nicht ascii zeichen ausfiltern 
        //        //     * nachteile ?! 
        //        //     * französische , mexikanische, .... namen könnten zerstört werden 
        //        //     * nach bearbeitung mit achten auf wort zusammen hänghe 
        //        //     * ascii wird nur ausgescnitten wenns nicht mitten im wort steht ?! 
        //        //     * schwer 
        //        //     * wie unterscheidet sich blub[] von blúb ? 
        //        //     * muss ich dann eine nachbetrachtung machen ?! 
        //        //     * die nicht ascii zeichen zählen und nur gerade anzahlen rausschnedien =! 
        //        //     * das doof 
        //        //     * damit wird mann nur klammern finden und das ist nichtz ausreichened 
        //        //     * 
        //        //     * wirf alle klammern raus in denen nix mehr drin steht 
        //        //     * also strings der form () , [] ob im,am wort ist egal 
        //        //     * 
        //        //     * 
        //        //     */
        //        //    return Wu;
        //        //}


        //        //private IEnumerable<List<TreeItem<ISSC>>> IterateBlock(WorkUnit wu)
        //        //{
        //        //    MaxLevelEnumerator<ISSC> iter = new MaxLevelEnumerator<ISSC>(wu.Data, 1, true);
        //        //    yield return wu.Data.Children;

        //        //    while (iter.MoveNext())
        //        //    {
        //        //        yield return iter.Current.Children;
        //        //    }
        //        //    iter = new MaxLevelEnumerator<ISSC>(wu.Data, 2, true);

        //        //    while (iter.MoveNext())
        //        //    {
        //        //        yield return iter.Current.Children;
        //        //    }
        //        //    yield break;
        //        //}


    }
}
