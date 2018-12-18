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
    public class PizzaIngredientsController : Controller
    {
        private readonly PizzaDB2Context _context;

        public PizzaIngredientsController(PizzaDB2Context context)
        {
            _context = context;
        }

        // GET: PizzaIngredients
        public async Task<IActionResult> Index()
        {
            var pizzaDB2Context = _context.PizzaIngredients.Include(p => p.Pizza);
            return View(await pizzaDB2Context.ToListAsync());
        }

        // GET: PizzaIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaIngredients = await _context.PizzaIngredients
                .Include(p => p.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaIngredients == null)
            {
                return NotFound();
            }

            return View(pizzaIngredients);
        }

        // GET: PizzaIngredients/Create
        public IActionResult Create()
        {
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName");
            return View();
        }

        // POST: PizzaIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PizzaId,IngredientName,IngredientCost")] PizzaIngredients pizzaIngredients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaIngredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName", pizzaIngredients.PizzaId);
            return View(pizzaIngredients);
        }

        // GET: PizzaIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaIngredients = await _context.PizzaIngredients.FindAsync(id);
            if (pizzaIngredients == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName", pizzaIngredients.PizzaId);
            return View(pizzaIngredients);
        }

        // POST: PizzaIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PizzaId,IngredientName,IngredientCost")] PizzaIngredients pizzaIngredients)
        {
            if (id != pizzaIngredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaIngredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaIngredientsExists(pizzaIngredients.Id))
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
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName", pizzaIngredients.PizzaId);
            return View(pizzaIngredients);
        }

        // GET: PizzaIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaIngredients = await _context.PizzaIngredients
                .Include(p => p.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaIngredients == null)
            {
                return NotFound();
            }

            return View(pizzaIngredients);
        }

        // POST: PizzaIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizzaIngredients = await _context.PizzaIngredients.FindAsync(id);
            _context.PizzaIngredients.Remove(pizzaIngredients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaIngredientsExists(int id)
        {
            return _context.PizzaIngredients.Any(e => e.Id == id);
        }
    }
}
