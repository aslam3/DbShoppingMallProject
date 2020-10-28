using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbShoppingMallProject.Data;
using DbShoppingMallProject.Models;

namespace DbShoppingMallProject.Controllers
{
    public class StallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stalls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stalls.Include(s => s.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stall = await _context.Stalls
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stall == null)
            {
                return NotFound();
            }

            return View(stall);
        }
        public IActionResult MultiCreate()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultiCreate(List<Stall> stalls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.AddRange(stalls);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return View(stalls);
            }
            return View(stalls);

        }
        // GET: Stalls/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Stalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StallNo,StallName,CatId,StallVolume,Level")] Stall stall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Id", stall.CatId);
            return View(stall);
        }

        // GET: Stalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stall = await _context.Stalls.FindAsync(id);
            if (stall == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Id", stall.CatId);
            return View(stall);
        }

        // POST: Stalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StallNo,StallName,CatId,StallVolume,Level")] Stall stall)
        {
            if (id != stall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StallExists(stall.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Id", stall.CatId);
            return View(stall);
        }

        // GET: Stalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stall = await _context.Stalls
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stall == null)
            {
                return NotFound();
            }

            return View(stall);
        }

        // POST: Stalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stall = await _context.Stalls.FindAsync(id);
            _context.Stalls.Remove(stall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StallExists(int id)
        {
            return _context.Stalls.Any(e => e.Id == id);
        }
    }
}
