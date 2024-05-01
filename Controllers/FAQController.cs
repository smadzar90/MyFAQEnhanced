using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using MyFAQEnhanced.Models;
using System.Diagnostics;

namespace MyFAQEnhanced.Controllers
{
    public class FAQController : Controller
    {
        private readonly ILogger<FAQController> _logger;
        private readonly FaqContext _context;

        public FAQController(ILogger<FAQController> logger, FaqContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {

            var faqs = await _context.FAQs.Include(m => m.Topic).Include(m => m.Category).ToListAsync();
            ViewBag.Count = faqs.Count;
            return View(faqs);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Topics = await _context.Topics.OrderBy(c => c.Name).ToListAsync();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FAQId,Question,Answer,TopicId,CategoryId")] FAQ faq)
        {

            if (ModelState.IsValid)
            {
                _context.Add(faq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Topics = await _context.Topics.OrderBy(c => c.Name).ToListAsync();
            return View(faq);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Topics = await _context.Topics.OrderBy(c => c.Name).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.FAQs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("FAQId,Question,Answer,TopicId,CategoryId")] FAQ faq)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQExists(faq.FAQId))
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

            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Topics = await _context.Topics.OrderBy(c => c.Name).ToListAsync();
            return View(faq);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.FAQs.Include(m => m.Category).Include(m => m.Topic).FirstOrDefaultAsync(m => m.FAQId == id);
            return View(faq);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.FAQs.Include(m => m.Category).Include(m => m.Topic).FirstOrDefaultAsync(m => m.FAQId == id);

            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFAQ(int? id)
        {
            var contact = await _context.FAQs.FindAsync(id);
            _context.FAQs.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> filterFAQs()
        {
            string searchItem = Request.Form["searchItem"];
            if (string.IsNullOrEmpty(searchItem))
            {
                return RedirectToAction(nameof(Search));
            }

            var faqs = await _context.FAQs.Where(m => m.Question.Contains(searchItem)).Include(m => m.Topic)
                .Include(m => m.Category).ToListAsync();
            ViewBag.Count = faqs.Count;

            return View("Index", faqs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool FAQExists(int id)
        {
            return _context.FAQs.Any(e => e.FAQId == id);
        }
    }
}
