using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbShoppingMallProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace DbShoppingMallProject.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext db;
        public SalariesController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            var data = db.Salaries.ToList();
            return Json(data);
        }
        [HttpPost]
        public IActionResult AddSalary(Salary sl)
        {
            
            Salary salary = new Salary
            {
                Designation = sl.Designation,
                Basic = sl.Basic,
                Medical = sl.Basic * (5m / 100),
                HouseRent = sl.Basic * (20m / 100)
            };
            try
            {
                db.Salaries.Add(salary);
                if (db.SaveChanges() > 0)
                {
                    return Json(new { result = "Success" });
                }
                else
                {
                    return Json(new { result = "Failed" });

                }
            }
            catch(Exception ex)
            {
                return ViewBag.Result = ex.Message;
            }
        }

        [HttpPost]
        public IActionResult UpdateSalary(Salary sl)
        {

            Salary salary = new Salary
            {
                Designation = sl.Designation,
                Basic = sl.Basic,
                Medical = sl.Basic * (5m / 100),
                HouseRent = sl.Basic * (20m / 100),
                Id=sl.Id
            };
            try
            {
                db.Entry(salary).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return Json(new { result = "Success" });
                }
                else
                {
                    return Json(new { result = "Failed" });

                }
            }
            catch (Exception ex)
            {
                return ViewBag.Result = ex.Message;
            }
        }

        public JsonResult Delete(int id)
        {
            var sl = db.Salaries.Find(id);
            db.Salaries.Remove(sl);
            if (db.SaveChanges() > 0)
            {
                return Json(new { result = "Success" });
            }
            else
            {
                return Json(new { result = "Failed" });
            }
        }

        public JsonResult GetById(int id)
        {
            var em = db.Salaries.Find(id);
            return Json(em);
        }
    }
}