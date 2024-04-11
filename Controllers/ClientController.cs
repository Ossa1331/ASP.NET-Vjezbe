using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Reflection;
using Vjezba.Web.Mock;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index(string query)
        {
            var clients = MockClientRepository.Instance.All().ToList();

            if (!string.IsNullOrEmpty(query))
            {
                clients = clients.Where(c => c.FullName.Contains(query)).ToList();
            }

            return View(clients);
        }
        [HttpPost]
        public IActionResult Index(string queryName, string queryAddress)
        {
            var clients = MockClientRepository.Instance.All().ToList();

            if (!string.IsNullOrEmpty(queryName))
            {
                clients = clients.Where(c => c.FullName.Contains(queryName)).ToList();
            }

            if (!string.IsNullOrEmpty(queryAddress))
            {
                clients = clients.Where(c => c.Address.Contains(queryAddress)).ToList();

            }
            return View(clients);
        }

        public IActionResult Details(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = MockClientRepository.Instance.FindByID(id.Value);

            if (client == null)
            {
                Client  nullClient= new Client();
                nullClient.ID = 999;
                return View(client);
            }

            return View(client);
        }
        public IActionResult AdvancedSearch(ClientFilterModel model)
        {
            var clients = MockClientRepository.Instance.All().ToList();

            if (!string.IsNullOrEmpty(model.Name))
            {
                clients = clients.Where(c => c.FullName.ToLower().Contains(model.Name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                clients = clients.Where(c => c.Email.ToLower().Contains(model.Email.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(model.Address))
            {
                clients = clients.Where(c => c.Address.ToLower().Contains(model.Address.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(model.CityName))
            {
                foreach(var client in clients)
                {
                    if (!string.IsNullOrEmpty(client.City.Name)){
                        clients = clients.
                        Where(c => c.City.Name.ToLower().Contains(model.CityName.ToLower())).ToList();
                    }
                }
            }
            return View("Index", clients);
        }

    }
}


