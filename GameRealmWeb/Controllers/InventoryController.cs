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
    public class InventoryController : Controller
    {
        private readonly Game_RealmContext _context;

        public InventoryController(Game_RealmContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var game_RealmContext = _context.Inventory.Include(i => i.Product).Include(i => i.Store);
            return View( game_RealmContext.ToList());
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _context.Inventory
                .Include(i => i.Product)
                .Include(i => i.Store)
                .FirstOrDefault(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title");
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("InventoryId,StoreId,ProductId,Quantity,Title")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title", inventory.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName", inventory.StoreId);
            return View(inventory);
        }

 
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _context.Inventory.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title", inventory.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName", inventory.StoreId);
            return View(inventory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("InventoryId,StoreId,ProductId,Quantity,Title")] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryId))
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
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title", inventory.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName", inventory.StoreId);
            return View(inventory);
        }

     
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _context.Inventory
                .Include(i => i.Product)
                .Include(i => i.Store)
                .FirstOrDefault(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var inventory = _context.Inventory.Find(id);
            _context.Inventory.Remove(inventory);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.InventoryId == id);
        }
    }
}
