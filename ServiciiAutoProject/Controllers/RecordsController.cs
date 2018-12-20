using Nexmo.Api;
using ServiciiAuto.DataLayer.Models;
using ServiciiAuto.DataLayer.Repository;
using ServiciiAutoProject.Helpers;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ServiciiAutoProject.Controllers
{
    public class RecordsController : Controller
    {
        private readonly RecordRepository _recordRepository = new RecordRepository();
        private readonly EnumsRepository _enumsRepository = new EnumsRepository();
        private readonly ClientRepository _clientRepository = new ClientRepository();

        public ActionResult Index()
        {
            //ImportRecords(@"C:\Users\petre\Downloads\export_365.csv");
            return View();
        }

        public ActionResult EditRecord()
        {
            ViewBag.RecordTypes = _enumsRepository.GetRecordTypes();
            ViewBag.VehicleTypes = _enumsRepository.GetVehicleTypes();
            ViewBag.ClientInformedStatueses = _enumsRepository.GetClientInformedStatueses();
            ViewBag.ClientsForDropDown = _clientRepository.GetAllClients();

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

        public ActionResult GetClientData(Guid clientId)
        {
            return Json(new { clientData = _clientRepository.GetClientById(clientId) });
        }

        public ActionResult ImportRecords(HttpPostedFileBase file)
        {
            var filePath = string.Empty;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                filePath = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(filePath);
            }

            var records = new CsvReaderHelper().GetRecordsFromCsv(filePath);
            foreach (var record in records)
            {
                _recordRepository.SaveImportedRecord(record);
            }
            
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Redirect("Index");
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