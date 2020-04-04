using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameRealm.DataAccess.Model;
using GameRealm.Domain.Model;

namespace GameRealmWeb.Controllers
{
    public class GamesController : Controller
    {
        private readonly Game_RealmContext _context;

        public GamesController(Game_RealmContext context)
        {
            _context = context;
        }

        // GET: Games
        public IActionResult Index()
        {
            return View( _context.Games.ToList());
        }

        // GET: Games/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games =  _context.Games
                .FirstOrDefault(m => m.ProductId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,Title,Genre,Release,Price,Quantity")] Games games)
        {
            if (ModelState.IsValid)
            {
                _context.Add(games);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(games);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = _context.Games.Find(id);
            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,Title,Genre,Release,Price,Quantity")] Games games)
        {
            if (id != games.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(games);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesExists(games.ProductId))
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
            return View(games);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = _context.Games
                .FirstOrDefault(m => m.ProductId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var games =  _context.Games.Find(id);
            _context.Games.Remove(games);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.ProductId == id);
        }
    }
}
