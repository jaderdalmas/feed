using Feed.Data;
using Feed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Feed.Controllers
{
    public class FeedItemController : Controller
    {
        private readonly FeedContext _context;

        public FeedItemController(FeedContext context)
        {
            _context = context;
        }

        // GET: FeedItem
        public async Task<IActionResult> Index(string searchString = "")
        {
            var items = from i in _context.FeedItemModel select i;

            if (!string.IsNullOrEmpty(searchString))
                items = items.Where(s => s.Title.Contains(searchString));

            return View(await items.ToListAsync());
        }

        // GET: FeedItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedItemModel = await _context.FeedItemModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedItemModel == null)
            {
                return NotFound();
            }

            return View(feedItemModel);
        }

        // GET: FeedItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeedItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Details")] FeedItemModel feedItemModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedItemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedItemModel);
        }

        // GET: FeedItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedItemModel = await _context.FeedItemModel.FindAsync(id);
            if (feedItemModel == null)
            {
                return NotFound();
            }
            return View(feedItemModel);
        }

        // POST: FeedItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Details")] FeedItemModel feedItemModel)
        {
            if (id != feedItemModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedItemModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedItemModelExists(feedItemModel.Id))
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
            return View(feedItemModel);
        }

        // GET: FeedItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedItemModel = await _context.FeedItemModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedItemModel == null)
            {
                return NotFound();
            }

            return View(feedItemModel);
        }

        // POST: FeedItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedItemModel = await _context.FeedItemModel.FindAsync(id);
            _context.FeedItemModel.Remove(feedItemModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedItemModelExists(int id)
        {
            return _context.FeedItemModel.Any(e => e.Id == id);
        }
    }
}
