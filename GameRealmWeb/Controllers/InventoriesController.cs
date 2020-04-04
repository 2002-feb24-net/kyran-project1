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
    public class InventoriesController : Controller
    {
        private readonly Game_RealmContext ctx;

        public InventoriesController(Game_RealmContext context)
        {
            ctx = context;
        }

        // GET: Inventories
        public IActionResult Index()
        {
            var game_RealmContext = ctx.Inventory.Include(i => i.Product).Include(i => i.Store);
            return View( game_RealmContext.ToList());
        }

        // GET: Inventories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory =  ctx.Inventory
                .Include(i => i.Product)
                .Include(i => i.Store)
                .FirstOrDefault(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(ctx.Games, "ProductId", "Title");
            ViewData["StoreId"] = new SelectList(ctx.Locations, "StoreId", "StoreName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("InventoryId,StoreId,ProductId,Quantity,Title")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                ctx.Add(inventory);
                 ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(ctx.Games, "ProductId", "Title", inventory.ProductId);
            ViewData["StoreId"] = new SelectList(ctx.Locations, "StoreId", "StoreName", inventory.StoreId);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory =  ctx.Inventory.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(ctx.Games, "ProductId", "Title", inventory.ProductId);
            ViewData["StoreId"] = new SelectList(ctx.Locations, "StoreId", "StoreName", inventory.StoreId);
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
                    ctx.Update(inventory);
                     ctx.SaveChanges();
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
            ViewData["ProductId"] = new SelectList(ctx.Games, "ProductId", "Title", inventory.ProductId);
            ViewData["StoreId"] = new SelectList(ctx.Locations, "StoreId", "StoreName", inventory.StoreId);
            return View(inventory);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory =  ctx.Inventory
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
            var inventory =  ctx.Inventory.Find(id);
            ctx.Inventory.Remove(inventory);
             ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return ctx.Inventory.Any(e => e.InventoryId == id);
        }
    }
}
