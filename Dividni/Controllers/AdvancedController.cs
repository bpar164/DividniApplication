using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dividni.Data;
using Dividni.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Dividni.Controllers
{
    [Authorize]
    public class AdvancedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdvancedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Advanced
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            //Fetch the user's question banks
            var banks = from b in _context.QuestionBank
                         where b.UserEmail.Equals(User.Identity.Name)
                         select new { b.Id, b.Name };

            var questionBanks = await banks.ToListAsync();
       
            ViewData["QuestionBanks"] = JsonSerializer.Serialize<Object>(questionBanks);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var advanced = from a in _context.Advanced
                         where a.UserEmail.Equals(User.Identity.Name)
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                advanced = advanced.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    advanced = advanced.OrderByDescending(a => a.Name);
                    break;
                case "name":
                    advanced = advanced.OrderBy(a => a.Name);
                    break;
                default:
                    advanced = advanced.OrderByDescending(a => a.ModifiedDate);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Advanced>.CreateAsync(advanced.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Marks,Question,UserEmail,ModifiedDate")] Advanced advanced)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Marks,Question,UserEmail,ModifiedDate")] Advanced advanced)
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
