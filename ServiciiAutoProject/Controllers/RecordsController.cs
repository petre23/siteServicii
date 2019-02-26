using DataLayer.Models;
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
            ViewBag.ClientsForDropDown = _clientRepository.GetAllClients();
            ViewBag.RecordTypes = _enumsRepository.GetRecordTypes(); 
            return View();
        }

        public ActionResult EditRecord()
        {
            if (Session != null && Session["RoleLevel"] != null && int.Parse(Session["RoleLevel"].ToString()) == 1)
            {
                ViewBag.RecordTypes = _enumsRepository.GetRecordTypes();
                ViewBag.VehicleTypes = _enumsRepository.GetVehicleTypes();
                ViewBag.ClientInformedStatueses = _enumsRepository.GetClientInformedStatueses();
                ViewBag.ClientsForDropDown = _clientRepository.GetAllClients();

                return View();
            }

            return null;
        }

        public ActionResult SaveRecord(Record record)
        {
            var userId = Session["UserId"].ToString();
            _recordRepository.SaveRecord(record, Guid.Parse(userId));
            return Json(new { saved = true });
        }

        public ActionResult GetRecords(string pageIndex, string pageSize, FilterModel filters)
        {
            return Json(new { records = _recordRepository.GetAllRecords(filters, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex) - 1) });
        }

        public ActionResult DeleteRecord(Guid recordId)
        {
            _recordRepository.DeleteRecord(recordId);
            return Json(new { success = "true" });
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
                _recordRepository.SaveImportedRecord(record, Session["UserId"].ToString());
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