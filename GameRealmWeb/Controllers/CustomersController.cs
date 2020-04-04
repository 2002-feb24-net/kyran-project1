using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GameRealm.DataAccess.Model;
using GameRealm.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameRealmWeb.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Game_RealmContext ctx;

        public CustomersController(Game_RealmContext context)
        {
            ctx = context;
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View( ctx.Customer.ToList());
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =  ctx.Customer
                .FirstOrDefault(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerId,FirstName,LastName,Phone,Email,Street,City,State,ZipCode,Password,UserName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                ctx.Add(customer);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = ctx.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerId,FirstName,LastName,Phone,Email,Street,City,State,ZipCode,Password,UserName")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ctx.Update(customer);
                     ctx.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }


        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =  ctx.Customer
                .FirstOrDefault(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            var customer =  ctx.Customer.Find(id);
            ctx.Customer.Remove(customer);
             ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return ctx.Customer.Any(e => e.CustomerId == id);
        }

        public  IActionResult Orders()
        {
            var game_RealmContext = ctx.Orders.Include(o => o.Customer).Include(o => o.Store);
            return View( game_RealmContext.ToList());
        }

        public IActionResult Search(string searchString)
        {
            var custFind = from m in ctx.Customer
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                custFind = custFind.Where(s => s.FirstName.ToUpper().Contains(searchString));
            }

            return View( custFind.ToList());
        }
    }
}
