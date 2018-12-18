using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LNPizza.DataAccess;

namespace LNPizza.Models
{
    public class StoreIngredientsController : Controller
    {
        private readonly PizzaDB2Context _context;

        public StoreIngredientsController(PizzaDB2Context context)
        {
            _context = context;
        }

        // GET: StoreIngredients
        public async Task<IActionResult> Index()
        {
            var pizzaDB2Context = _context.StoreIngredients.Include(s => s.StoreIngredientsAddress);
            return View(await pizzaDB2Context.ToListAsync());
        }

        // GET: StoreIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeIngredients = await _context.StoreIngredients
                .Include(s => s.StoreIngredientsAddress)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeIngredients == null)
            {
                return NotFound();
            }

            return View(storeIngredients);
        }

        // GET: StoreIngredients/Create
        public IActionResult Create()
        {
            ViewData["StoreIngredientsAddressId"] = new SelectList(_context.Store, "StoreAddressId", "Address1");
            return View();
        }

        // POST: StoreIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreIngredientsAddressId,Id,IngredientStock")] StoreIngredients storeIngredients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeIngredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreIngredientsAddressId"] = new SelectList(_context.Store, "StoreAddressId", "Address1", storeIngredients.StoreIngredientsAddressId);
            return View(storeIngredients);
        }

        // GET: StoreIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeIngredients = await _context.StoreIngredients.FindAsync(id);
            if (storeIngredients == null)
            {
                return NotFound();
            }
            ViewData["StoreIngredientsAddressId"] = new SelectList(_context.Store, "StoreAddressId", "Address1", storeIngredients.StoreIngredientsAddressId);
            return View(storeIngredients);
        }

        // POST: StoreIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreIngredientsAddressId,Id,IngredientStock")] StoreIngredients storeIngredients)
        {
            if (id != storeIngredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeIngredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreIngredientsExists(storeIngredients.Id))
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
            ViewData["StoreIngredientsAddressId"] = new SelectList(_context.Store, "StoreAddressId", "Address1", storeIngredients.StoreIngredientsAddressId);
            return View(storeIngredients);
        }

        // GET: StoreIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeIngredients = await _context.StoreIngredients
                .Include(s => s.StoreIngredientsAddress)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeIngredients == null)
            {
                return NotFound();
            }

            return View(storeIngredients);
        }

        // POST: StoreIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeIngredients = await _context.StoreIngredients.FindAsync(id);
            _context.StoreIngredients.Remove(storeIngredients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreIngredientsExists(int id)
        {
            return _context.StoreIngredients.Any(e => e.Id == id);
        }
    }
}
