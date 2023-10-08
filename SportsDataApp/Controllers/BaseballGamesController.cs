using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsDataApp.Data;
using SportsDataApp.Models;

namespace SportsDataApp.Controllers
{
    public class BaseballGamesController : Controller
    {
        private readonly SportsDataAppContext _context;

        public BaseballGamesController(SportsDataAppContext context)
        {
            _context = context;
        }

        // GET: BaseballGames
        public async Task<IActionResult> Index()
        {
            var sportsDataAppContext = _context.BaseballGame.Include(b => b.Season);
            return View(await sportsDataAppContext.ToListAsync());
        }

        // GET: BaseballGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaseballGame == null)
            {
                return NotFound();
            }

            var baseballGame = await _context.BaseballGame
                .Include(b => b.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseballGame == null)
            {
                return NotFound();
            }

            return View(baseballGame);
        }

        // GET: BaseballGames/Create
        public IActionResult Create()
        {
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id");
            return View();
        }

        // POST: BaseballGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RunsScored,RunsAllowed,WorL,SeasonId")] BaseballGame baseballGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baseballGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", baseballGame.SeasonId);
            return View(baseballGame);
        }

        // GET: BaseballGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaseballGame == null)
            {
                return NotFound();
            }

            var baseballGame = await _context.BaseballGame.FindAsync(id);
            if (baseballGame == null)
            {
                return NotFound();
            }
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", baseballGame.SeasonId);
            return View(baseballGame);
        }

        // POST: BaseballGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RunsScored,RunsAllowed,WorL,SeasonId")] BaseballGame baseballGame)
        {
            if (id != baseballGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baseballGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseballGameExists(baseballGame.Id))
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
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", baseballGame.SeasonId);
            return View(baseballGame);
        }

        // GET: BaseballGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaseballGame == null)
            {
                return NotFound();
            }

            var baseballGame = await _context.BaseballGame
                .Include(b => b.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseballGame == null)
            {
                return NotFound();
            }

            return View(baseballGame);
        }

        // POST: BaseballGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaseballGame == null)
            {
                return Problem("Entity set 'SportsDataAppContext.BaseballGame'  is null.");
            }
            var baseballGame = await _context.BaseballGame.FindAsync(id);
            if (baseballGame != null)
            {
                _context.BaseballGame.Remove(baseballGame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaseballGameExists(int id)
        {
          return (_context.BaseballGame?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
