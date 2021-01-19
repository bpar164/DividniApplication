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

            int pageSize = 9;
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

        // POST: QuestionBank/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<Boolean> DeleteConfirmed(Guid id)
        {
            var questionBank = await _context.QuestionBank.FindAsync(id);
            if (questionBank != null) {
                _context.QuestionBank.Remove(questionBank);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // POST: QuestionBank/Edit
        [HttpPost]
        public async Task<Boolean> Edit(QuestionBank questionBank)
        {
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
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                return true;
            }
            return false;
        }

        private bool QuestionBankExists(Guid id)
        {
            return _context.QuestionBank.Any(e => e.Id == id);
        }
    }
}
