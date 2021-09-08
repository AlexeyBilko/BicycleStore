using BicycleStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BicycleStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BicycleStore.Controllers
{
    public class HomeController : Controller
    {
        BicycleContext db;

        public HomeController(BicycleContext context)
        {
            db = context;
        }

        public IActionResult Index()//string company = "all", int? page = 1, SortType sortType = SortType.ManufacturerAsc)
        {
            //ViewBag.TotalPages = company == "all" ? (int)Math.Ceiling(db.Bicycles.ToList().Count / (double)5) : Math.Ceiling(db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).ToList().Count / (double)5);
            //var selectListItems = new List<string>
            //{
            //    "all"
            //};
            //selectListItems.AddRange(db.Bicycles.Select(x => x.Manufacturer));

            //ViewBag.PageNumber = page;

            //List<Bicycle> items = new List<Bicycle>();
            //switch (sortType)
            //{
            //    case SortType.ManufacturerAsc:
            //        items = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Manufacturer).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Manufacturer).ToList();
            //        break;
            //    case SortType.ModelAsc:
            //        items = company == "all" ? db.Bicycles.OrderBy(x => x.Model).Skip((int)((page - 1) * 5)).Take(5).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).OrderBy(x => x.Model).Skip((int)((page - 1) * 5)).Take(5).ToList();
            //        break;
            //    case SortType.WeightAsc:
            //        items = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Weight).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Weight).ToList();
            //        break;
            //    case SortType.WeelsRadiusAsc:
            //        items = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.WeelsRadius).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.WeelsRadius).ToList();
            //        break;
            //    case SortType.BrakesAsc:
            //        items = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Brakes).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Brakes).ToList();
            //        break;
            //    case SortType.TypeAsc:
            //        items = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Type).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Type).ToList();
            //        break;
            //    case SortType.PriceAsc:
            //        items = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Price).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Skip((int)((page - 1) * 5)).Take(5).OrderBy(x => x.Price).ToList();
            //        break;
            //    default:
            //        break;
            //}

            //if (items.Count == 0)
            //{
            //    ViewBag.PageNumber = 1;
            //    return View(new BicycleListViewModel
            //    {
            //        Bicycles = company == "all" ? db.Bicycles.Take(5).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Take(5).ToList(),
            //        Companies = new SelectList(selectListItems)
            //    });
            //}

            //return View(new BicycleListViewModel
            //{
            //    Bicycles = items,
            //    Companies = new SelectList(selectListItems)
            //});
            //int TotalPages = (int)Math.Ceiling(db.Bicycles.ToList().Count / (double)5);
            //int? PageNumber = 1;

            //var selectListItems = new List<string>
            //{
            //    "all"
            //};
            //selectListItems.AddRange(db.Bicycles.Select(x => x.Manufacturer));

            //return View(new BicycleListViewModel
            //{
            //    Bicycles = db.Bicycles.Take(5).ToList(),
            //    Companies = new SelectList(selectListItems),
            //    PageNumber = PageNumber,
            //    TotalPages = TotalPages
            //});
            return View();

        }

        public IActionResult BicyclesList(string search = "", int? page = 1, string company = "all")
        {
            int TotalPages = company == "all" ? (int)Math.Ceiling(db.Bicycles.ToList().Count / (double)5) : (int)Math.Ceiling(db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).ToList().Count / (double)5);
            int? PageNumber = page;
            var selectListItems = new List<string>
            {
                "all"
            };
            selectListItems.AddRange(db.Bicycles.Select(x => x.Manufacturer));

            var bicycles = company == "all" ? db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).ToList() : db.Bicycles.Where(x => x.Manufacturer.ToLower() == company.ToLower()).Take(5).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                bicycles = bicycles.Where(x => x.Manufacturer.ToLower().Contains(search.ToLower())).Take(5).ToList();
                TotalPages = (int)Math.Ceiling(bicycles.ToList().Count / (double)5);
                PageNumber = 1;
            }

            


            return PartialView(new BicycleListViewModel
            {
                Bicycles = bicycles,
                Companies = new SelectList(selectListItems),
                PageNumber = PageNumber,
                TotalPages = TotalPages
            });
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.BicycleId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Buy(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                try
                {
                    MailAddress from = new MailAddress("bilkoalexey@gmail.com", "Tom");
                    MailAddress to = new MailAddress(order.Email);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "ORDER - Bicycle Shop";
                    m.Body = $"Hi {order.Name} {order.Surname}\n\nYour order has been accepted and awaiting dispatch\n\n2021 BicycleShop";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("bilkoalexey@gmail.com", "***********************");
                    smtp.EnableSsl = true;
                    smtp.SendMailAsync(m);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Thanks for purchase");
            }
            else
            {
                return View();
            }
        }
    }
}


            
