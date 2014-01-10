using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbstractDataExtraction;
using System.IO;
using Common.ISSC;
using Tree;
using FileStructureDataExtraction.Builder;
using Interfaces;

namespace FileStructureDataExtraction.Tests.Builder
{
    [TestClass]
    public class TreeBuilderTests
    {

        private List<IVirtualFile> CreateTestData(string baseURL,string artist ,int albums, int songs)
        {
            List<IVirtualFile> data = new List<IVirtualFile>();
            // LayerDataResultBase
            string temp = "";
            for (int i = 0; i < albums; i++)
            {
                for (int j = 0; j < songs; j++)
                {
                    temp = String.Format("{0}\\{1}\\album{2}\\song{3}.mp3", baseURL, artist, i, j);
                    data.Add(new LayerDataResultBase(i * 100 + j, new FileInfo(temp)));
                }
            }
            return data;
        }

        private TreeItem<InformationStorageSet> BuildTestResultTree(string artist, int albums, int songs)
        {

            return null;
        }


        [TestMethod]
        public void should_build_tree_from_enum()
        {
            TreeBuilder builder = new TreeBuilder();
            string rootPath = "C:\\dummy";
            var testData = CreateTestData(rootPath,"In Flames", 2, 4);
            var result = BuildTestResultTree("In Flames", 2,4);
            builder.Build(rootPath.Length, testData);

            
        }        

    }
}
