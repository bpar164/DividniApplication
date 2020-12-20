using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dividni.Data;
using Dividni.Models;

namespace Dividni.Controllers
{
    public class AdvancedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdvancedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Advanced
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advanced.ToListAsync());
        }

        // GET: Advanced/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advanced = await _context.Advanced
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advanced == null)
            {
                return NotFound();
            }

            return View(advanced);
        }

        // GET: Advanced/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advanced/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Marks,Question,UserEmail")] Advanced advanced)
        {
            if (ModelState.IsValid)
            {
                advanced.Id = Guid.NewGuid();
                _context.Add(advanced);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advanced);
        }

        // GET: Advanced/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advanced = await _context.Advanced.FindAsync(id);
            if (advanced == null)
            {
                return NotFound();
            }
            return View(advanced);
        }

        // POST: Advanced/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Marks,Question,UserEmail")] Advanced advanced)
        {
            if (id != advanced.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advanced);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvancedExists(advanced.Id))
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
            return View(advanced);
        }

        // GET: Advanced/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advanced = await _context.Advanced
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advanced == null)
            {
                return NotFound();
            }

            return View(advanced);
        }

        // POST: Advanced/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var advanced = await _context.Advanced.FindAsync(id);
            _context.Advanced.Remove(advanced);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvancedExists(Guid id)
        {
            return _context.Advanced.Any(e => e.Id == id);
        }
    }
}
