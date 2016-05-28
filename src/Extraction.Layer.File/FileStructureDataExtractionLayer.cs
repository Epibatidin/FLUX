using DataStructure.Tree.Iterate;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using System;
using System.Collections.Generic;
using FileTreeItem = DataStructure.Tree.TreeItem<Extraction.Layer.File.FileLayerSongDo>;

namespace Extraction.Layer.File
{
    public class FileStructureDataExtractionLayer : IDataExtractionLayer
    {
        private readonly IEnumerable<IPartedStringOperation> _cleaners;
        private readonly ITreeByKeyAccessorBuilder _byTreeAccessorBuilder;

        public FileStructureDataExtractionLayer(IEnumerable<IPartedStringOperation> cleaners, ITreeByKeyAccessorBuilder byTreeAccessorBuilder)
        {
            _cleaners = cleaners;
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
        }

        private void ForeachPartedStringInTree(FileTreeItem root, Func<PartedString, PartedString> func)
        {
            foreach (var item in TreeIterator.IterateDepthGetTreeItems(root))
            {
                item.Value.LevelValue = func(item.Value.LevelValue);
            }
        }


        //        FileLayerConfig _config;

        //        private FileItem _data;

        //        private List<Action> _work;

        //        public override void Execute()
        //        {
        //            foreach (var item in _work)
        //            {
        //                item();
        //                Update();
        //            }
        //        }

        //        protected override void ConfigureInternal(XmlReader reader)
        //        {
        //            _config = new FileLayerConfig(reader);
        //            _work = new List<Action>();
        //            _work.Add(InternetStuff); // internet zeug entfernen ist wahrscheinlich das leichteste und fehlerfreiste 
        //            _work.Add(BlackList);
        //            _work.Add(ExtractTrack);
        //            _work.Add(ExtractYear);


        //            CreateProgress(_work.Count);
        //        }

        //        protected override ISongByKeyAccessor Data()
        //        {
        //            return new TreeByKeyAccessor(_data);
        //        }

        //        public override void InitData(Dictionary<int, IVirtualFile> dirtyData)
        //        {
        //            // daten aufbereiten 
        //            // wird nich neu gelesen ! 
        //            // bau den baum 
        //            TreeByKeyAccessorBuilder builder = new TreeByKeyAccessorBuilder();
        //            _data = builder.Build(dirtyData.Values);
        //            Update();
        //            // dem data store bescheid geben ? 
        //        }

        //        
        //        ///<summary>
        //        /// Die Erste Funktion die ausgeführt - 
        //        /// Entfernt Urls
        //        ///</summary>
        //        ///<returns></returns>
        //        private void InternetStuff()
        //        {
        //            InternetStuffPartedStringOperation cleaner = new InternetStuffPartedStringOperation();
        //            ForeachPartedStringInTree(_data, cleaner.Operate);            
        //        }


        //        /// <summary>
        //        /// Zweite Function
        //        /// </summary>
        //        /// <param name="W"></param>
        //        /// <returns></returns>
        //        private void BlackList()
        //        {
        //            RemoveBlackListValuesOperation cleaner = new RemoveBlackListValuesOperation(_config.BlackListConfig);
        //            ForeachPartedStringInTree(_data, cleaner.Operate);
        //        }

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



        //        //private void RemoveDoubles()
        //        //{
        //        //    EqualityCleaner cleaner = new EqualityCleaner(_config.WhiteListConfig);




        //        //    List<PartedString> currentBlockData = null;
        //        //    foreach (var block in IterateBlock(W))
        //        //    {
        //        //        currentBlockData = new List<PartedString>();
        //        //        foreach (var treeItem in block)
        //        //        {
        //        //            treeItem.Value.Status = Common.DataStatus.Updated;
        //        //            currentBlockData.Add(treeItem.Value.CleanIIS.getByKey<PartedString>(treeItem.Level));
        //        //        }
        //        //        cleaner.Operate(currentBlockData);
        //        //        var z = currentBlockData;
        //        //    }
        //        //    return W;
        //        //}

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
