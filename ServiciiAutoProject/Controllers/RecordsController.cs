using Nexmo.Api;
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
            ViewBag.ClientsForDropDown = new ClientRepository().GetAllClients();

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
            return Json(new { record = _recordRepository.GetRecordById(recordId) });
        }

        public ActionResult SendMessageToClient(Record record)
        {
            try
            {
                var messageForClient = string.Format($"Buna ziua dl/dna {record.ClientName}. \n\r Va informam ca asigurarea dvs urmeaza sa expire pe data de {record.ExpirationDateString}. \n\r Va asteptam la noi pentru a prelungi valabilitatea acesteia. O zi buna. Echipa MobaTehnic. ");

                var client = new Nexmo.Api.Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "e12689ef",
                    ApiSecret = "P9iflKUmKi9lbjkE"
                });
                var results = client.SMS.Send(request: new SMS.SMSRequest
                {
                    from = "MobaTehnic",
                    to = "40760073998",
                    text = string.Format($"Va informam ca asigurarea dvs urmeaza sa expire pe data de {record.ExpirationDateString}. \n\r O zi buna. Echipa MobaTehnic.")
                });

                return Json(new { messageSent = true });
            }
            catch (Exception ex)
            {
                return Json(new { messageSent = false });
            }
        }
    }
}