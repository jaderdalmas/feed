using Feed.Data;
using Feed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Feed.Controllers
{
    public class FeedController : Controller
    {
        private readonly FeedContext _context;

        public FeedController(FeedContext context)
        {
            _context = context;
        }

        // GET: Feed
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeedModel.Include(x => x.Items).ToListAsync());
        }

        // GET: Feed/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedModel = await _context.FeedModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedModel == null)
            {
                return NotFound();
            }

            return View(feedModel);
        }

        // GET: Feed/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] FeedModel feedModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedModel);
        }

        // GET: Feed/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedModel = await _context.FeedModel.FindAsync(id);
            if (feedModel == null)
            {
                return NotFound();
            }
            return View(feedModel);
        }

        // POST: Feed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] FeedModel feedModel)
        {
            if (id != feedModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedModelExists(feedModel.Id))
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
            return View(feedModel);
        }

        // GET: Feed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedModel = await _context.FeedModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedModel == null)
            {
                return NotFound();
            }

            return View(feedModel);
        }

        // POST: Feed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedModel = await _context.FeedModel.FindAsync(id);
            _context.FeedModel.Remove(feedModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedModelExists(int id)
        {
            return _context.FeedModel.Any(e => e.Id == id);
        }
    }
}
