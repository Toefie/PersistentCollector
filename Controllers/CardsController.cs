using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMVC.Controllers
{
    public class CardsController : Controller
    {
        private readonly PCollectorDB _context;

        public CardsController(PCollectorDB context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index(string searchString, int? minPrice, int? maxPrice, int? psa)
        {
            // Begin met een query van alle kaarten
            var cardsQuery = _context.Cards.Include(c => c.Collections).AsQueryable();

            // Filteren op zoekterm (naam van de kaart, case-insensitive)
            if (!string.IsNullOrEmpty(searchString))
            {
                cardsQuery = cardsQuery.Where(c => c.Name.ToLower().Contains(searchString.ToLower()));
            }

            // Filteren op minimum prijs
            if (minPrice.HasValue)
            {
                cardsQuery = cardsQuery.Where(c => c.Price >= minPrice.Value);
            }

            // Filteren op maximum prijs
            if (maxPrice.HasValue)
            {
                cardsQuery = cardsQuery.Where(c => c.Price <= maxPrice.Value);
            }

            // Filteren op PSA waarde
            if (psa.HasValue)
            {
                cardsQuery = cardsQuery.Where(c => c.Psa == psa.Value);
            }

            // Haal de gefilterde kaarten op
            var cards = await cardsQuery.ToListAsync();

            // Maak een ViewModel voor elke kaart inclusief de collecties
            var viewModel = cards.Select(card => new CollectionCardView
            {
                Card = card,
                Collections = card.Collections.ToList()
            }).ToList();

            return View(viewModel);
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FirstOrDefaultAsync(m => m.ID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            // Haal de collectie namen op in plaats van ID's
            ViewBag.CollectionName = new SelectList(_context.Collections, "Name", "Name");
            return View("CreateEdit", new CardCreateEditViewModel());
        }

        // POST: Cards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,Description,Psa,BuyDate,CurrentPrice,Specialty,CollectionName")] CardCreateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var card = new Card
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    Psa = viewModel.Psa,
                    BuyDate = viewModel.BuyDate,
                    CurrentPrice = viewModel.CurrentPrice,
                    Specialty = viewModel.Specialty,
                    // Verbind de kaart aan de geselecteerde collectie
                    Collections = _context.Collections.Where(c => c.Name == viewModel.CollectionName).ToList()
                };

                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CollectionName = new SelectList(_context.Collections, "Name", "Name", viewModel.CollectionName);
            return View("CreateEdit", viewModel);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.Include(c => c.Collections).FirstOrDefaultAsync(m => m.ID == id);
            if (card == null)
            {
                return NotFound();
            }

            var viewModel = new CardCreateEditViewModel
            {
                ID = card.ID,
                Name = card.Name,
                Price = card.Price,
                Description = card.Description,
                Psa = card.Psa,
                BuyDate = card.BuyDate,
                CurrentPrice = card.CurrentPrice,
                Specialty = card.Specialty,
                CollectionName = card.Collections.FirstOrDefault()?.Name // Zet de naam van de collectie
            };

            ViewBag.CollectionName = new SelectList(_context.Collections, "Name", "Name", viewModel.CollectionName);
            return View("CreateEdit", viewModel);
        }

        // POST: Cards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Description,Psa,BuyDate,CurrentPrice,Specialty,CollectionName")] CardCreateEditViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var card = await _context.Cards.Include(c => c.Collections).FirstOrDefaultAsync(c => c.ID == id);
                    if (card == null)
                    {
                        return NotFound();
                    }

                    card.Name = viewModel.Name;
                    card.Price = viewModel.Price;
                    card.Description = viewModel.Description;
                    card.Psa = viewModel.Psa;
                    card.BuyDate = viewModel.BuyDate;
                    card.CurrentPrice = viewModel.CurrentPrice;
                    card.Specialty = viewModel.Specialty;

                    // Update de collectie
                    card.Collections.Clear();
                    card.Collections = _context.Collections.Where(c => c.Name == viewModel.CollectionName).ToList();

                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(viewModel.ID))
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

            ViewBag.CollectionName = new SelectList(_context.Collections, "Name", "Name", viewModel.CollectionName);
            return View("CreateEdit", viewModel);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FirstOrDefaultAsync(m => m.ID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.ID == id);
        }
    }
}