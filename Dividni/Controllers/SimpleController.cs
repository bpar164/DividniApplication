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
    public class SimpleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SimpleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Simple
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

            var simple = from s in _context.Simple
                         where s.UserEmail.Equals(User.Identity.Name)
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                simple = simple.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    simple = simple.OrderByDescending(s => s.Name);
                    break;
                case "name":
                    simple = simple.OrderBy(s => s.Name);
                    break;
                default:
                    simple = simple.OrderByDescending(s => s.ModifiedDate);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Simple>.CreateAsync(simple.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        private bool SimpleExists(Guid id)
        {
            return _context.Simple.Any(e => e.Id == id);

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

        // POST: Simple/Share
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share([Bind("Id,Name,Type,Marks,QuestionText,CorrectAnswers,IncorrectAnswers,UserEmail,ModifiedDate")] Simple simple)
        {
            if (!UserExists(simple.UserEmail))
            {
                ViewData["Message"] = "Could not find user with email: " + simple.UserEmail;
                return View(simple);
            }
            else
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
        }

        private bool UserExists(string email)
        {
            return _context.Users.Any(u => u.UserName == email);
        }
    }
}
