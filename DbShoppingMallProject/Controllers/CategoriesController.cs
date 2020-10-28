using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbShoppingMallProject.Data;
using DbShoppingMallProject.Models;
using DbShoppingMallProject.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DbShoppingMallProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment ihost;

        public CategoriesController(ApplicationDbContext db, IHostingEnvironment host)
        {
            this.db = db;
            this.ihost = host;
        }


        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            ViewBag.CatId = new SelectList(db.Categories.ToList(), "Id", "Name");
            List<CatStallVm> data = new List<CatStallVm>();
            foreach(Category category in db.Categories)
            {
                data.Add(new CatStallVm
                {
                    Category = category.Name,
                    Description = category.Description,
                    Stalls = db.Stalls.Where(p => p.CatId == category.Id).ToList()
                });
            }
            return View(data);
        }
        public IActionResult Update(IFormCollection data)
        {
            Stall s = new Stall
            {
                StallName = data["txtname"],
                StallVolume = int.Parse(data["txtvolum"]),
                Level = (Level)Enum.Parse(typeof(Level), data["txtLevel"]),
                Id = int.Parse(data["txtId"]),
                CatId=int.Parse(data["txtcat"])
            };
            db.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetAll");

        }
    }
}