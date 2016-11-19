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
        
        //        //private WorkUnit OneStepUp(WorkUnit W)
        //        //{
        //        //    OneStepUpCleaner cleaner = new OneStepUpCleaner();

        //        //    cleaner.Operate(W.Data);

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
    }
}
