using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MP3Renamer.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            // alle dateien aus moq laden 

            // proceed 

            // schreiben 

            // jedes einzelne file ergleichen => aufwand 

            // ordnung erhalten ?! 
            // ordner für ordner kann nicht gelesen werden 
            // jeder artists kann für sich validiert werden 

            // keine zusammenhänge zwischen artisten

            // artisten unterscheiden können setzt struktur analyse vorraus => abgelehnt
 
            // am besten wäre die einzelnen teile einzeln zu validieren 

            // und wieder wo bekomm ich die ordnung her ? 
            // im testbench kann man schon davon ausgehen das die alphabetische ordnung "absolut" ist

            // ich könnte in jedem ordner ablegen was rauskommen soll

            // das ist gut 
            // darauf kann eine absolute ordnung definiert werden 
            // und der finalizer könnte für den testbetrieb konfiguriert werden

            // so dass er die gleichen dateien schreibt 




            return View();
        }

    }
}
