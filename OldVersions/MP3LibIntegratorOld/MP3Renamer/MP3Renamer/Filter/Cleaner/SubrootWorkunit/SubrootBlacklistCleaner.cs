//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using MP3Renamer.Filter.Interfaces;
//using MP3Renamer.DataContainer.EntityInterfaces;

//namespace MP3Renamer.Filter.Cleaner.SubrootWorkunit
//{
//    public class SubrootBlacklistCleaner :  ISubrootWorkunit , IExecuteable
//    {
//        public ISubRoot Workunit { get; set; }

//        public void Execute()
//        {
//            // hier den extraction algo anstoßen 
//            EqualityCleaner algo = new EqualityCleaner();

//            var list = Workunit.Leafs.Select(c => c.StringManager).ToList();

//            list = algo.Filter(list);

//            for (int i = 0; i < list.Count; i++)
//            {
//                Workunit.Leafs[i].StringManager = list[i];
//            }
//        }
//    }
//    }
//}