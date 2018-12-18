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
    public class UserTblsController : Controller
    {
        private readonly PizzaDB2Context _context;

        public UserTblsController(PizzaDB2Context context)
        {
            _context = context;
        }

        // GET: UserTbls
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserTbl.ToListAsync());
        }

        // GET: UserTbls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTbl = await _context.UserTbl
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTbl == null)
            {
                return NotFound();
            }

            return View(userTbl);
        }

        // GET: UserTbls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName")] UserTbl userTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTbl);
        }

        // GET: UserTbls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTbl = await _context.UserTbl.FindAsync(id);
            if (userTbl == null)
            {
                return NotFound();
            }
            return View(userTbl);
        }

        // POST: UserTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName")] UserTbl userTbl)
        {
            if (id != userTbl.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTblExists(userTbl.UserId))
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
            return View(userTbl);
        }

        // GET: UserTbls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTbl = await _context.UserTbl
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTbl == null)
            {
                return NotFound();
            }

            return View(userTbl);
        }

        // POST: UserTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTbl = await _context.UserTbl.FindAsync(id);
            _context.UserTbl.Remove(userTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTblExists(int id)
        {
            return _context.UserTbl.Any(e => e.UserId == id);
        }
    }
}
