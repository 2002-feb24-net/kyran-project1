using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRealm.DataAccess;
using GameRealm.DataAccess.Model;
using GameRealm.Domain.Model;
using GameRealmWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameRealmWeb.Controllers
{

  
    public class LoginController : Controller  
    {
        private CustomerDAL _cust;

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CustomersViewModel currUser)
        {
            Customer currentCustomer = _cust.FindByFirstName(currUser.FirstName);
            if(ModelState.IsValid)
            {
                if(currUser != null)
                {
                    if(currentCustomer.FirstName == currUser.FirstName)
                    {
                        HttpContext.Session.SetString("AcctID", currentCustomer.CustomerId.ToString());
                        return Redirect("/Locations/Index");
                    }
                }
            }
            return View(currUser);
        }

    }
}
