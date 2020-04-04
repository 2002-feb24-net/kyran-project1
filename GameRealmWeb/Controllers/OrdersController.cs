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
    public class OrdersController : Controller
    {
        private readonly Game_RealmContext _context;

        public OrdersController(Game_RealmContext context)
        {
            _context = context;
        }

        // GET: Orders
        public IActionResult Index()
        {
            var game_RealmContext = _context.Orders.Include(o => o.Customer).Include(o => o.Store);
            return View( game_RealmContext.ToList());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders =  _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .FirstOrDefault(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName");
            ViewData["ProductID"] = new SelectList(_context.Games, "ProductId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,StoreId,CustomerId,ProductId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                orders.Time = DateTime.Now;
                _context.Add(orders);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName", orders.StoreId);
            ViewData["ProductID"] = new SelectList(_context.Games, "ProductId", "Title");
            return View(orders);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders =  _context.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName", orders.StoreId);
            return View(orders);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderId,StoreId,CustomerId,Time,Checkout")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName", orders.StoreId);
            return View(orders);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders =  _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .FirstOrDefault(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var orders =  _context.Orders.Find(id);
            _context.Orders.Remove(orders);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        public IActionResult Option()
        {
            return View();
        }


    }
}
