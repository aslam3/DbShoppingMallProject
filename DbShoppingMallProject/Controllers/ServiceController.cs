using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbShoppingMallProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace DbShoppingMallProject.Controllers
{
    public class ServiceController : Controller
    {
       private readonly ApplicationDbContext db;
        public ServiceController(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            //var services = db.Services.Include(s => s.CompanyInfo);
            var proc = db.Services.FromSql<Services>("Sp_ServiceAllData");
            return View(proc);
        }
        public IActionResult Create()
        {
            ViewData["stallId"] = new SelectList(db.Stalls, "Id", "StallName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Services s)
        {
            ViewData["stallId"] = new SelectList(db.Stalls, "Id", "StallName",s.StallId);
            var proc = db.Database.ExecuteSqlCommand("Sp_ServiceCreate @title={0},@descrip={1},@stallId={2}",
                s.Title,s.Description,s.StallId);
            if (proc > 0)
            {
                return RedirectToAction("Index");
            }
            return View(s);
        }
        public IActionResult Edit(int id)
        {
            ViewData["stallId"] = new SelectList(db.Stalls, "Id", "StallName",db.Services.Find(id).StallId);
            var proc = db.Services.FromSql<Services>("Sp_ServiceDetails @id={0}", id).SingleOrDefault();
            return View(proc);

        }
        [HttpPost]
        public IActionResult Edit(Services s)
        {
            ViewData["stallId"] = new SelectList(db.Stalls, "Id", "StallName",s.StallId);
            var proc = db.Database.ExecuteSqlCommand("Sp_ServiceUpdate @title={0},@descrip={1},@stallId={2},@id={3}",
                s.Title, s.Description, s.StallId,s.Id);
            if (proc > 0)
            {
                return RedirectToAction("Index");
            }
            return View(s);
            
        }
        public IActionResult Delete(int id)
        {
            

            var proc = db.Database.ExecuteSqlCommand("Sp_ServiceDelete @id={0}", id);
            if (proc > 0)
            {
                return RedirectToAction("Index"); 
            }
            return View("Index");
        }
        public IActionResult Details(int id)
        {
            var proc = db.Services.FromSql<Services>("Sp_ServiceDetails @id={0}", id).SingleOrDefault();
            return View(proc);
        }

    }
}