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
    public class OrderedPizzasController : Controller
    {
        private readonly PizzaDB2Context _context;

        public OrderedPizzasController(PizzaDB2Context context)
        {
            _context = context;
        }

        // GET: OrderedPizzas
        public async Task<IActionResult> Index()
        {
            var pizzaDB2Context = _context.OrderedPizzas.Include(o => o.IdNavigation).Include(o => o.Order).Include(o => o.Pizza);
            return View(await pizzaDB2Context.ToListAsync());
        }

        // GET: OrderedPizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedPizzas = await _context.OrderedPizzas
                .Include(o => o.IdNavigation)
                .Include(o => o.Order)
                .Include(o => o.Pizza)
                .FirstOrDefaultAsync(m => m.OrderId1 == id);
            if (orderedPizzas == null)
            {
                return NotFound();
            }

            return View(orderedPizzas);
        }

        // GET: OrderedPizzas/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Pizza, "Id", "PizzaName");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName");
            return View();
        }

        // POST: OrderedPizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,PizzaId,OrderId1")] OrderedPizzas orderedPizzas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderedPizzas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Pizza, "Id", "PizzaName", orderedPizzas.Id);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderedPizzas.OrderId);
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName", orderedPizzas.PizzaId);
            return View(orderedPizzas);
        }

        // GET: OrderedPizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedPizzas = await _context.OrderedPizzas.FindAsync(id);
            if (orderedPizzas == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Pizza, "Id", "PizzaName", orderedPizzas.Id);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderedPizzas.OrderId);
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName", orderedPizzas.PizzaId);
            return View(orderedPizzas);
        }

        // POST: OrderedPizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,PizzaId,OrderId1")] OrderedPizzas orderedPizzas)
        {
            if (id != orderedPizzas.OrderId1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderedPizzas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderedPizzasExists(orderedPizzas.OrderId1))
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
            ViewData["Id"] = new SelectList(_context.Pizza, "Id", "PizzaName", orderedPizzas.Id);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderedPizzas.OrderId);
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "PizzaName", orderedPizzas.PizzaId);
            return View(orderedPizzas);
        }

        // GET: OrderedPizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedPizzas = await _context.OrderedPizzas
                .Include(o => o.IdNavigation)
                .Include(o => o.Order)
                .Include(o => o.Pizza)
                .FirstOrDefaultAsync(m => m.OrderId1 == id);
            if (orderedPizzas == null)
            {
                return NotFound();
            }

            return View(orderedPizzas);
        }

        // POST: OrderedPizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderedPizzas = await _context.OrderedPizzas.FindAsync(id);
            _context.OrderedPizzas.Remove(orderedPizzas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderedPizzasExists(int id)
        {
            return _context.OrderedPizzas.Any(e => e.OrderId1 == id);
        }
    }
}
