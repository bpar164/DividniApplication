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

namespace Dividni.Controllers
{
    [Authorize]
    public class SimpleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SimpleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Simple
        public async Task<IActionResult> Index(string searchString)
        {
            var simple =
                from s in _context.Simple
                where s.UserEmail.Equals(User.Identity.Name)
                orderby s.ModifiedDate
                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                simple = simple.Where(s => s.Name.Contains(searchString)).OrderByDescending(s => s.ModifiedDate);
            }

            return View(await simple.ToListAsync());
        }

        // GET: Simple/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _context.Simple
                .FirstOrDefaultAsync(m => m.Id == id);
            if (simple == null)
            {
                return NotFound();
            }

            return View(simple);
        }

        // GET: Simple/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Simple/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Marks,QuestionText,CorrectAnswers,IncorrectAnswers,UserEmail,ModifiedDate")] Simple simple)
        {
            if (ModelState.IsValid)
            {
                simple.Id = Guid.NewGuid();
                _context.Add(simple);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(simple);
        }

        // GET: Simple/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _context.Simple.FindAsync(id);
            if (simple == null)
            {
                return NotFound();
            }
            return View(simple);
        }

        // POST: Simple/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Marks,QuestionText,CorrectAnswers,IncorrectAnswers,UserEmail,ModifiedDate")] Simple simple)
        {
            if (id != simple.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(simple);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SimpleExists(simple.Id))
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
            return View(simple);
        }

        // GET: Simple/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _context.Simple
                .FirstOrDefaultAsync(m => m.Id == id);
            if (simple == null)
            {
                return NotFound();
            }

            return View(simple);
        }

        // POST: Simple/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var simple = await _context.Simple.FindAsync(id);
            _context.Simple.Remove(simple);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Simple/Template/5
        public async Task<IActionResult> Template(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _context.Simple.FindAsync(id);
            if (simple == null)
            {
                return NotFound();
            }
            return View(simple);
        }

        // POST: Simple/Template
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Template([Bind("Id,Name,Type,Marks,QuestionText,CorrectAnswers,IncorrectAnswers,UserEmail,ModifiedDate")] Simple simple)
        {
            if (ModelState.IsValid)
            {
                simple.Id = Guid.NewGuid();
                _context.Add(simple);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(simple);
        }

        // GET: Simple/Share/5
        public async Task<IActionResult> Share(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _context.Simple
                .FirstOrDefaultAsync(m => m.Id == id);
            if (simple == null)
            {
                return NotFound();
            }

            return View(simple);
        }

        private bool SimpleExists(Guid id)
        {
            return _context.Simple.Any(e => e.Id == id);
        }
    }
}
