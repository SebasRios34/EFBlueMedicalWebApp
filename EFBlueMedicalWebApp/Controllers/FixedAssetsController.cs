using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFBlueMedicalWebApp.Data;
using EFBlueMedicalWebApp.Models;

namespace EFBlueMedicalWebApp.Controllers
{
    public class FixedAssetsController : Controller
    {
        private readonly DBContext _context;

        public FixedAssetsController(DBContext context)
        {
            _context = context;
        }

        // GET: FixedAssets
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.FixedAssets.Include(f => f.Department);
            return View(await dBContext.ToListAsync());
        }

        // GET: FixedAssets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssets = await _context.FixedAssets
                .Include(f => f.Department)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (fixedAssets == null)
            {
                return NotFound();
            }

            return View(fixedAssets);
        }

        // GET: FixedAssets/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");
            return View();
        }

        // POST: FixedAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetID,AssetName,DepartmentID,DateCreated,LastUsed,UsefulLife,Price,DepreciationRate,DepreciatedAmount")] FixedAssets fixedAssets)
        {
            if (ModelState.IsValid)
            {
                var vidaUtil = fixedAssets.UsefulLife;
                var precio = fixedAssets.Price;


                var depreRate = 1 / vidaUtil;
                depreRate = depreRate * 2;
                var depreAmount = precio * depreRate;

                fixedAssets.DepreciationRate = depreRate;
                fixedAssets.DepreciatedAmount = depreAmount;

                fixedAssets.DateCreated = System.DateTime.Today;
                fixedAssets.LastUsed = System.DateTime.Today;

                _context.Add(fixedAssets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID", fixedAssets.DepartmentID);
            return View(fixedAssets);
        }

        // GET: FixedAssets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssets = await _context.FixedAssets.FindAsync(id);
            if (fixedAssets == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID", fixedAssets.DepartmentID);
            return View(fixedAssets);
        }

        // POST: FixedAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetID,AssetName,DepartmentID,DateCreated,LastUsed,UsefulLife,Price,DepreciationRate,DepreciatedAmount")] FixedAssets fixedAssets)
        {
            if (id != fixedAssets.AssetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixedAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixedAssetsExists(fixedAssets.AssetID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID", fixedAssets.DepartmentID);
            return View(fixedAssets);
        }

        // GET: FixedAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssets = await _context.FixedAssets
                .Include(f => f.Department)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (fixedAssets == null)
            {
                return NotFound();
            }

            return View(fixedAssets);
        }

        // POST: FixedAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedAssets = await _context.FixedAssets.FindAsync(id);
            _context.FixedAssets.Remove(fixedAssets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedAssetsExists(int id)
        {
            return _context.FixedAssets.Any(e => e.AssetID == id);
        }
    }
}
