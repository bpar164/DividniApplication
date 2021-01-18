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
    public class QuestionBankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionBankController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuestionBank
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
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

            var banks = from b in _context.QuestionBank
                         where b.UserEmail.Equals(User.Identity.Name)
                         select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                banks = banks.Where(b => b.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    banks = banks.OrderByDescending(s => s.Name);
                    break;
                case "name":
                    banks = banks.OrderBy(s => s.Name);
                    break;
                default:
                    banks = banks.OrderByDescending(s => s.ModifiedDate);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<QuestionBank>.CreateAsync(banks.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // POST: QuestionBank/Create
        [HttpPost]
        public async Task<Boolean> Create([Bind("Id,Name,QuestionList,UserEmail,ModifiedDate")] QuestionBank questionBank) 
        {
            if (ModelState.IsValid)
            {
                questionBank.Id = Guid.NewGuid();
                _context.Add(questionBank);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // GET: QuestionBank/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionBank = await _context.QuestionBank.FindAsync(id);
            if (questionBank == null)
            {
                return NotFound();
            }
            return View(questionBank);
        }

        // POST: QuestionBank/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,QuestionList,UserEmail,ModifiedDate")] QuestionBank questionBank)
        {
            if (id != questionBank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionBankExists(questionBank.Id))
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
            return View(questionBank);
        }

        // GET: QuestionBank/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionBank = await _context.QuestionBank
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionBank == null)
            {
                return NotFound();
            }

            return View(questionBank);
        }

        // POST: QuestionBank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var questionBank = await _context.QuestionBank.FindAsync(id);
            _context.QuestionBank.Remove(questionBank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionBankExists(Guid id)
        {
            return _context.QuestionBank.Any(e => e.Id == id);
        }
    }
}
