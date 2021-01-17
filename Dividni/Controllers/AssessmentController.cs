using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dividni.Data;
using Dividni.Models;
using Dividni.Services;
using Microsoft.AspNetCore.Authorization;

namespace Dividni.Controllers
{
    [Authorize]
    public class AssessmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AssessmentService _service;

        public AssessmentController(ApplicationDbContext context)
        {
            _context = context;
            _service = new AssessmentService(context);
        }

        // GET: Assessment
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

            var assessment = from a in _context.Assessment
                             where a.UserEmail.Equals(User.Identity.Name)
                             select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                assessment = assessment.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    assessment = assessment.OrderByDescending(s => s.Name);
                    break;
                case "name":
                    assessment = assessment.OrderBy(s => s.Name);
                    break;
                default:
                    assessment = assessment.OrderByDescending(s => s.ModifiedDate);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Assessment>.CreateAsync(assessment.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Assessment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // GET: Assessment/Create
        public async Task<IActionResult> Create(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionBank = await _context.QuestionBank
                .FirstOrDefaultAsync(q => q.Id == id);
            if (questionBank == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["QuestionBank"] = questionBank.QuestionList;
            }

            return View();
        }

        // POST: Assessment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CoverPage,QuestionList,Appendix,UserEmail,ModifiedDate")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                assessment.Id = Guid.NewGuid();
                _context.Add(assessment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assessment);
        }

        // Post: Assessment/Question
        [HttpPost]
        public async Task<IActionResult> Question(Question questionDetails)
        {
            if (questionDetails.id == "")
            {
                return null;
            }

            try
            {
                var id = new Guid(questionDetails.id);
                if (questionDetails.type == "Simple")
                {
                    var question = await _context.Simple
                        .FirstOrDefaultAsync(q => q.Id == id);
                        return Ok(question);
                }
                else
                {
                    var question = await _context.Advanced
                        .FirstOrDefaultAsync(q => q.Id == id); 
                        return Ok(question);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: Assessment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessment.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        // POST: Assessment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CoverPage,QuestionList,Appendix,UserEmail,ModifiedDate")] Assessment assessment)
        {
            if (id != assessment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assessment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssessmentExists(assessment.Id))
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
            return View(assessment);
        }

        private bool AssessmentExists(Guid id)
        {
            return _context.Assessment.Any(e => e.Id == id);
        }

        // GET: Assessment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // POST: Assessment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var assessment = await _context.Assessment.FindAsync(id);
            _context.Assessment.Remove(assessment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Assessment/Template/5
        public async Task<IActionResult> Template(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessment.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        // POST: Assessment/Template
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Template([Bind("Id,Name,CoverPage,QuestionList,Appendix,UserEmail,ModifiedDate")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                assessment.Id = Guid.NewGuid();
                _context.Add(assessment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assessment);
        }

        // GET: Assessment/Share/5
        public async Task<IActionResult> Share(Guid? id, string message)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessment == null)
            {
                return NotFound();
            }

            if (message != null) {
                ViewData["Message"] = message;
            }
            return View(assessment);
        }

        // POST: Assessment/Share
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share([Bind("Id,Name,CoverPage,QuestionList,Appendix,UserEmail,ModifiedDate")] Assessment assessment)
        {
            if (!UserExists(assessment.UserEmail))
            {
                ViewData["Message"] = "Could not find user with email: " + assessment.UserEmail;
                return View(assessment);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    assessment.Id = Guid.NewGuid();
                    _context.Add(assessment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(assessment);
            }
        }

        // POST: Assessment/ShareAll
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShareAll([Bind("Id,Name,CoverPage,QuestionList,Appendix,UserEmail,ModifiedDate")] Assessment assessment)
        {
            if (!UserExists(assessment.UserEmail))
            {
                return RedirectToAction(nameof(Share), new { id = assessment.Id, message = "Could not find user with email: " + assessment.UserEmail});
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var questionList = _service.shareAllQuestions(assessment.QuestionList, assessment.UserEmail, assessment.ModifiedDate);
                    assessment.Id = Guid.NewGuid();
                    assessment.QuestionList = questionList;
                    _context.Add(assessment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Share), new { id = assessment.Id });
            }
        }

        private bool UserExists(string email)
        {
            return _context.Users.Any(u => u.UserName == email);
        }

        // GET: Assessment/Download/5
        public async Task<IActionResult> Download(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // POST: Assessment/Generate
        [HttpPost]
        public async Task<Boolean> Generate(DownloadRequest downloadRequest)
        {
            var assessment = await _context.Assessment
                .FirstOrDefaultAsync(m => m.Id == downloadRequest.Id);
            if (assessment == null)
            {
                return false;
            }
            else
            {
                return _service.generateAssessment(assessment, downloadRequest);
            }
        }

        // GET: Assessment/DownloadAssessment/name
        public FileResult DownloadAssessment(string name)
        {
            return File(_service.getAssessmentFile(name), "application/zip", name + ".zip");
        }
    }
}
