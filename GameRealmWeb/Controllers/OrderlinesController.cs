using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameRealm.DataAccess.Model;
using GameRealm.Domain.Model;
using GameRealm.DataAccess;

namespace GameRealmWeb.Controllers
{
    public class OrderlinesController : Controller
    {
        private readonly Game_RealmContext _context;
        private readonly OrdersDAL ordsDAL = new OrdersDAL();

        public OrderlinesController(Game_RealmContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var game_RealmContext = _context.Orderline.Include(o => o.Order).Include(o => o.Product);
            return View(game_RealmContext.ToList());
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderline = _context.Orderline
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefault(m => m.OrderlineId == id);
            if (orderline == null)
            {
                return NotFound();
            }

            return View(orderline);
        }


        public IActionResult Create()
        {
            var lastOrderPlaced = ordsDAL.preOrder();
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,OrderId,Quantity")] Orderline orderline)
        {

            var lastOrderPlaced = ordsDAL.preOrder();
            orderline.OrderId = lastOrderPlaced.OrderId;
            if (ModelState.IsValid)
            {
                _context.Orderline.Add(orderline);
                _context.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            ViewData["Quantity"] = new SelectList(_context.Orders, "Quantity", "Quantity", orderline.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title", orderline.ProductId);
            return View(orderline);
        }

 
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderline = _context.Orderline.Find(id);
            if (orderline == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderline.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title", orderline.ProductId);
            return View(orderline);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,OrderId,Quantity,OrderlineId")] Orderline orderline)
        {
            if (id != orderline.OrderlineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderline);
                     _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!OrderlineExists(orderline.OrderlineId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderline.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Games, "ProductId", "Title", orderline.ProductId);
            return View(orderline);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderline = _context.Orderline
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefault(m => m.OrderlineId == id);
            if (orderline == null)
            {
                return NotFound();
            }

            return View(orderline);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var orderline = _context.Orderline.Find(id);
            _context.Orderline.Remove(orderline);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderlineExists(int id)
        {
            return _context.Orderline.Any(e => e.OrderlineId == id);
        }


    }
}
