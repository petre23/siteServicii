using ServiciiAuto.DataLayer.Models;
using System.Web.Mvc;

namespace ServiciiAutoProject.Controllers
{
    public class RecordsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditRecord()
        {
            return View();
        }

        public ActionResult SaveRecord(Record record)
        {
            return null;
        }
    }
}