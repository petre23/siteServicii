using ServiciiAuto.DataLayer.Models;
using ServiciiAuto.DataLayer.Repository;
using System;
using System.Web.Mvc;

namespace ServiciiAutoProject.Controllers
{
    public class ClientController : Controller
    {
        private ClientRepository _clientRepository = new ClientRepository();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditClient()
        {
            if (Session != null && Session["RoleLevel"] != null && int.Parse(Session["RoleLevel"].ToString()) == 1)
            {
                return View();
            }
            return null;
        }

        public ActionResult SaveClient(Client client)
        {
            _clientRepository.SaveClient(client);
            return Json(new { saved = true });
        }

        public ActionResult GetClients()
        {
            return Json(new { clients = _clientRepository.GetAllClients() });
        }

        public ActionResult GetClientById(Guid clientId)
        {
            return Json(new { client = _clientRepository.GetClientById(clientId) });
        }

        public ActionResult DeleteClient(Guid clientId)
        {
            _clientRepository.DeleteClient(clientId);
            return Json(new { success = true });
        }
    }
}