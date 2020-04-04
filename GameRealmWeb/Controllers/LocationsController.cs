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
    public class LocationsController : Controller
    {
        private readonly Game_RealmContext ctx;

        public LocationsController(Game_RealmContext context)
        {
            ctx = context;
        }

        // GET: Locations
        public IActionResult Index()
        {
            return View( ctx.Locations.ToListAsync());
        }

        // GET: Locations/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations =  ctx.Locations
                .FirstOrDefault(m => m.StoreId == id);
            if (locations == null)
            {
                return NotFound();
            }

            return View(locations);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                ctx.Add(locations);
                 ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(locations);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations =  ctx.Locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }
            return View(locations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Locations locations)
        {
            if (id != locations.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ctx.Update(locations);
                     ctx.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationsExists(locations.StoreId))
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
            return View(locations);
        }

        // GET: Locations/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations =  ctx.Locations
                .FirstOrDefault(m => m.StoreId == id);
            if (locations == null)
            {
                return NotFound();
            }

            return View(locations);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var locations =  ctx.Locations.Find(id);
            ctx.Locations.Remove(locations);
             ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationsExists(int id)
        {
            return ctx.Locations.Any(e => e.StoreId == id);
        }
    }
}
