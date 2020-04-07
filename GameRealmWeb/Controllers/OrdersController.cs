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
using System.Text.Json;
using GameRealmWeb.ViewModels;
using System.Collections;
using GameRealm.Interface;

namespace GameRealmWeb.Controllers
{
    public class OrdersController : Controller
    {

        OrdersDAL orderRepo = new OrdersDAL();
        private readonly Game_RealmContext _context;
        private readonly GamesDAL _game;
        private readonly Customer _cust;

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
        public IActionResult Create([Bind("LocationId,CustomerId,ProductID")] OrdersViewModel ordersV)
        {
            Game_RealmContext ctx = new Game_RealmContext();

            if (ModelState.IsValid)
            {
                string orderlinesController = "Orderlines";
                Orders newOrd = new Orders
                {
                    
                    CustomerId = ordersV.CustomerId,
                    StoreId = ordersV.LocationId,
                    Time = DateTime.Now,
                    Checkout = 0
                };



                _context.Add(newOrd);
                _context.SaveChanges();
                _context.Entry(newOrd).Reload();
                Orderline ords = new Orderline
                {
                    OrderId = newOrd.OrderId,
                    ProductId = ordersV.ProductID,
                    Quantity = 1,
                };

                _context.Orderline.Add(ords);
                _context.SaveChanges();
                return RedirectToAction(nameof(Create), orderlinesController);


            }

            

            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["StoreId"] = new SelectList(_context.Locations, "StoreId", "StoreName");
            ViewData["ProductID"] = new SelectList(_context.Games, "ProductId", "Title");
            ViewData["Price"] = new SelectList(_context.Games, "Price", "Price");
            return RedirectToAction(nameof(Index));
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

        public IActionResult Checkout()
        {
           /*List<int>
           */
            return View();
        }

        public IActionResult CustOrderSearch(string searchString)
        {
            var custOrder = from m in _context.Orders
                           where m.Customer.FirstName == searchString || m.Customer.LastName == searchString
                           select m;

            var customerOrder = custOrder.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                custOrder = custOrder.Where(s => s.Customer.FirstName.ToUpper().Contains(searchString) || s.Customer.LastName.ToUpper().Contains(searchString));
            }
            else if (searchString == null)
            {
                return View("Index");
            }
            else
            {
                return View("Search");
            }

            return View(customerOrder);
        }

        public IActionResult custOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custOrderhistory = from cust in _context.Orders
                                   where cust.CustomerId == id
                                   select cust;
/*
            var custHistory = _context.Orders.Where(c => c.CustomerId == id);*/
            if (custOrderhistory == null)
            {
                return NotFound();
            }

            return View(custOrderhistory);
        }


        public IActionResult AddToCart([Bind("ProductId")] Orderline orders)
        {
            List<int> gamesAdded = JsonSerializer.Deserialize<List<int>>((string)TempData["cart"]);

            if (gamesAdded == null)
            {
                gamesAdded = new List<int> { orders.ProductId };
            }
            else
            {
                gamesAdded.Add(orders.ProductId);
            }

            TempData["cart"] = JsonSerializer.Serialize(gamesAdded);



            return View();
        }



    }
}
