using ServiciiAuto.DataLayer.Models;
using ServiciiAuto.DataLayer.Repository;
using System;
using System.Web.Mvc;

namespace ServiciiAutoProject.Controllers
{
    public class RecordsController : Controller
    {
        private readonly RecordRepository _recordRepository = new RecordRepository();
        private readonly EnumsRepository _enumsRepository = new EnumsRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditRecord()
        {
            ViewBag.RecordTypes = _enumsRepository.GetRecordTypes();
            ViewBag.VehicleTypes = _enumsRepository.GetVehicleTypes();
            ViewBag.ClientInformedStatueses = _enumsRepository.GetClientInformedStatueses();

            return View();
        }

        public ActionResult SaveRecord(Record record)
        {
            _recordRepository.SaveRecord(record);
            return Json(new { saved = true });
        }

        public ActionResult GetRecords()
        {
            return Json(new { records = _recordRepository.GetAllRecords() });
        }

        public ActionResult GetRecordsById(Guid recordId)
        {
            return Json(new { selectedRecord = _recordRepository.GetRecordById(Guid.Empty) });
        }
    }
}