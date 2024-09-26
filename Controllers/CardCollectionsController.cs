using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    public class CardCollectionsController : Controller
    {
        private readonly PCollectorDB _context;

        public CardCollectionsController(PCollectorDB context)
        {
            _context = context;
        }

        // GET: CardCollections
        public async Task<IActionResult> Index()
        {
            var pCollectorDB = _context.CardCollections.Include(c => c.Card).Include(c => c.Collection);
            return View(await pCollectorDB.ToListAsync());
        }

        // GET: CardCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardCollection = await _context.CardCollections
                .Include(c => c.Card)
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (cardCollection == null)
            {
                return NotFound();
            }

            return View(cardCollection);
        }

        // GET: CardCollections/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "ID", "ID");
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id");
            return View();
        }

        // POST: CardCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,CollectionId")] CardCollection cardCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "ID", "ID", cardCollection.CardId);
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id", cardCollection.CollectionId);
            return View(cardCollection);
        }

        // GET: CardCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardCollection = await _context.CardCollections.FindAsync(id);
            if (cardCollection == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "ID", "ID", cardCollection.CardId);
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id", cardCollection.CollectionId);
            return View(cardCollection);
        }

        // POST: CardCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardId,CollectionId")] CardCollection cardCollection)
        {
            if (id != cardCollection.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardCollectionExists(cardCollection.CardId))
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
            ViewData["CardId"] = new SelectList(_context.Cards, "ID", "ID", cardCollection.CardId);
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id", cardCollection.CollectionId);
            return View(cardCollection);
        }

        // GET: CardCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardCollection = await _context.CardCollections
                .Include(c => c.Card)
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (cardCollection == null)
            {
                return NotFound();
            }

            return View(cardCollection);
        }

        // POST: CardCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardCollection = await _context.CardCollections.FindAsync(id);
            if (cardCollection != null)
            {
                _context.CardCollections.Remove(cardCollection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardCollectionExists(int id)
        {
            return _context.CardCollections.Any(e => e.CardId == id);
        }
    }
}
