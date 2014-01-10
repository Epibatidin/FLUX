using System.Collections.Generic;
using System.Web.Mvc;
using MP3Renamer.Config;
using MP3Renamer.Models;
using MP3Renamer.ViewModels;
using MP3Renamer.FileIO;


namespace MP3Renamer.Controllers
{
    public class HomeController : Controller
    {

        private FileListViewModel FLVM
        {
            get
            {
                return (FileListViewModel)Session["FLVM"];
            }
            set
            {
                Session["FLVM"] = value;
            }
        }

        public ActionResult Index()
        {
            return FileList(); //  View("Index");
        }

        [NonAction]
        private ActionResult FileListing()
        {
            if (FLVM.IsValid)
                return View("FileList", FLVM);
            else
                return View("Error", FLVM.Exceptions);
        }


        public ActionResult FileList()
        {
            FileManager.Refresh(Configuration.SourceFolder);
            FLVM = null;
            FLVM = new FileListViewModel();

            return FileListing();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SetRoots(List<byte> Roots)
        {
            FileManager.Get.SelectedRoots = Roots;
            return FileListing();
        }


        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult Proceed()
        //{
        //    if (FLVM == null)
        //        FLVM = new FileListViewModel();

        //    FLVM.Proceed();

        //    return FileListing();
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proceed()
        {
            FLVM.Proceed();

            return FileListing();
        }




        public ActionResult WriteToFS(int CopyMode)
        {
            
            FileStructureFactory fac = new FileStructureFactory((CopyType)CopyMode);
            fac.WriteToFileSystem();
            //var Writer = new DataWriter(CopyMode);
            //Writer.WriteToFileSystem();
            //return View("WriteInProcess", DataManager.Get.LeafCount);
            return FileListing();
        }
    }
}
