using BicycleStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using BicycleStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BicycleStore.Controllers
{
    //[AllowAnonymous]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        BicycleContext db;
        IdentityContext IdentityContext;
        public AdminController(BicycleContext context, IdentityContext identityContext)
        {
            this.db = context;
            this.IdentityContext = identityContext;
        }
        public IActionResult Index(int?page = 1)
        {
            ViewBag.TotalPages = (int)Math.Ceiling(db.Bicycles.ToList().Count / (double)5);
            if (page == null || page <= 0)
            {
                ViewBag.PageNumber = 1;
                return View(db.Bicycles.Take(5).ToList());
            }
            ViewBag.PageNumber = page;
            var items = db.Bicycles.Skip((int)((page - 1) * 5)).Take(5).ToList();
            if (items.Count == 0)
            {
                ViewBag.PageNumber = 1;
                return View(db.Bicycles.Take(5).ToList());
            }
            return View(db.Bicycles.ToList());
            //return View(db.Bicycles.ToList());
        }

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                return View(db.Bicycles.FirstOrDefault(x => x.BicycleId == id));
            }
        }

        public IActionResult UsersManagement()
        {
            return View(IdentityContext.AspNetUsers.ToList());
        }

        public IActionResult CreateUsers(string id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                return View(IdentityContext.AspNetUsers.FirstOrDefault(x => x.Id == id));
            }
        }

        [HttpPost]
        public IActionResult CreateUsers(User user)
        {
            if (user.Id == "")
            {
                IdentityContext.AspNetUsers.Add(user);
            }
            else
            {
                var userEntity = IdentityContext.AspNetUsers.FirstOrDefault(x => x.Id == user.Id);
                userEntity.UserName = user.UserName;
                userEntity.Email = user.Email;
                userEntity.PasswordHash = user.PasswordHash;
                userEntity.PhoneNumber = user.PhoneNumber;
                IdentityContext.Entry(userEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            IdentityContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Bicycle bicycle)
        {
            if (bicycle.BicycleId == 0)
            {
                db.Bicycles.Add(bicycle);
            }
            else
            {
                var bicycleEntity = db.Bicycles.FirstOrDefault(x => x.BicycleId == bicycle.BicycleId);
                bicycleEntity.Manufacturer = bicycle.Manufacturer;
                bicycleEntity.Model = bicycle.Model;
                bicycleEntity.Price = bicycle.Price;
                bicycleEntity.WeelsRadius = bicycle.WeelsRadius;
                bicycleEntity.Weight = bicycle.Weight;
                bicycleEntity.Brakes = bicycle.Brakes;
                bicycleEntity.Type = bicycle.Type;
                db.Entry(bicycleEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int BicycleId)
        {
            var bicycleToDelete = db.Bicycles.Find(BicycleId);
            db.Bicycles.Remove(bicycleToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(string Id)
        {
            var userToRemove = IdentityContext.AspNetUsers.Find(Id);
            IdentityContext.AspNetUsers.Remove(userToRemove);
            IdentityContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
