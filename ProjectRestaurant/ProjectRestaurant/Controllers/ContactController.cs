using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;
        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactMailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad & Soyad: " + model.Name);
                body.AppendLine("E-Mail Adresi: " + model.Email);
                body.AppendLine("Konu: " + model.Subject);
                body.AppendLine("Mesaj: " + model.Message);
                _contactService.MailSender(body.ToString());
            }
            return View();
        }
    }
}