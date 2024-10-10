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
    public class CollectionsController : Controller
    {
        private readonly PCollectorDB _context;

        public CollectionsController(PCollectorDB context)
        {
            _context = context;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var pCollectorDB = _context.Collections.Include(c => c.Inventory);
            return View(await pCollectorDB.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.Inventory)
                .Include(c => c.Cards) // Include the Cards related to the Collection
                .FirstOrDefaultAsync(m => m.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            // Stel InventoryID standaard in op 1
            var model = new Collection
            {
                InventoryID = 1
            };

            return View(model);
        }

        // POST: Collections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InventoryID")] Collection collection, int[] selectedCards)
        {
            if (ModelState.IsValid)
            {
                // Add selected cards to the collection
                if (selectedCards != null)
                {
                    foreach (var cardId in selectedCards)
                    {
                        var card = await _context.Cards.FindAsync(cardId);
                        if (card != null)
                        {
                            collection.Cards.Add(card);
                        }
                    }
                }

                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryID"] = new SelectList(_context.Inventories, "ID", "ID", collection.InventoryID);
            ViewData["Cards"] = new SelectList(_context.Cards, "ID", "Name"); // Repopulate card list in case of error
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.Cards) // Include existing cards in the collection
                .FirstOrDefaultAsync(c => c.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            ViewData["InventoryID"] = new SelectList(_context.Inventories, "ID", "ID", collection.InventoryID);
            ViewData["Cards"] = new MultiSelectList(_context.Cards, "ID", "Name", collection.Cards.Select(c => c.ID)); // Preselect existing cards
            return View(collection);
        }

        // POST: Collections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InventoryID")] Collection collection, int[] selectedCards)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the collection's cards
                    var collectionToUpdate = await _context.Collections
                        .Include(c => c.Cards)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (collectionToUpdate != null)
                    {
                        collectionToUpdate.Name = collection.Name;
                        collectionToUpdate.InventoryID = collection.InventoryID;

                        // Clear existing cards
                        collectionToUpdate.Cards.Clear();

                        // Add the new selected cards
                        foreach (var cardId in selectedCards)
                        {
                            var card = await _context.Cards.FindAsync(cardId);
                            if (card != null)
                            {
                                collectionToUpdate.Cards.Add(card);
                            }
                        }

                        _context.Update(collectionToUpdate);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
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

            ViewData["InventoryID"] = new SelectList(_context.Inventories, "ID", "ID", collection.InventoryID);
            ViewData["Cards"] = new MultiSelectList(_context.Cards, "ID", "Name", selectedCards); // Repopulate cards in case of error
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.Inventory)
                .Include(c => c.Cards) // Include the cards in the collection
                .FirstOrDefaultAsync(m => m.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collections
                .Include(c => c.Cards) // Include the cards for removal
                .FirstOrDefaultAsync(c => c.Id == id);

            if (collection != null)
            {
                collection.Cards.Clear(); // Remove the cards from the collection before deleting
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.Id == id);
        }
    }
}
