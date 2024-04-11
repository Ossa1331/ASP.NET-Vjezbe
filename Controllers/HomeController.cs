﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> _logger) 
        : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Jednostavan način proslijeđivanja poruke iz Controller -> View.";
            //Kao rezultat se pogled /Views/Home/Contact.cshtml renderira u "pravi" HTML
            //Primjetiti - View() je poziv funkcije koja uzima cshtml template i pretvara ga u HTML
            //Zasto bas Contact.cshtml? Jer se akcija zove Contact, te prema konvenciji se "po defaultu" uzima cshtml datoteka u folderu Views/CONTROLLER_NAME/AKCIJA.cshtml


            return View();
        }

        /// <summary>
        /// Ova akcija se poziva kada na formi za kontakt kliknemo "Submit"
        /// URL ove akcije je /Home/SubmitQuery, uz POST zahtjev isključivo - ne može se napraviti GET zahtjev zbog [HttpPost] parametra
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {
            //Ovdje je potrebno obraditi podatke i pospremiti finalni string u ViewBag

            string fullName = formData["ime"];
            string email = formData["email"];
            string message = formData["poruka"];
            string messageType = formData["tipPoruke"];
            string newsletter = formData["newsletter"];

            string notification = 
                          $"Poštovani {fullName} ({email}) zaprimili smo vašu poruku te će vam se netko ubrzo javiti. " +
                          $"Sadržaj vaše poruke je: [{messageType}] {message}. " +
                          $"Također, {(newsletter == "on" ? "obavijestit ćemo vas" : "nećemo vas obavijestiti")} o daljnjim promjenama preko newslettera.";

            ViewBag.Notification = notification;
            //Kao rezultat se pogled /Views/Home/ContactSuccess.cshtml renderira u "pravi" HTML
            //Kao parametar se predaje naziv cshtml datoteke koju treba obraditi (ne koristi se default vrijednost)
            //Trazenu cshtml datoteku je potrebno samostalno dodati u projekt
            return View("ContactSuccess");
        }
        public IActionResult PrivacyLink()
        {
            var faqUrlWithSelected = Url.Action("FAQ", "Home", new { selected = 4 });
            var faqUrlWithoutSelected = Url.Action("FAQ", "Home");

            ViewBag.FAQUrlWithSelected = faqUrlWithSelected;
            ViewBag.FAQUrlWithoutSelected = faqUrlWithoutSelected;

            return View();
        }
        public IActionResult FAQ(int? selected)
        {
            // Provjera da li je postavljen parametar 'selected'
            ViewBag.SelectedQuestion = selected;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}