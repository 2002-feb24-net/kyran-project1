using GameRealm.Interface;
using System.Collections.Generic;
using System.Linq;
using GameRealm.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using GameRealm.DataAccess.Model;
using System.Threading.Tasks;

namespace GameRealm.DataAccess
{
    public class CustomerDAL : ICustomer
    //customer data access library
    {
        Game_RealmContext ctx = new Game_RealmContext();

        public void Add(Customer cust)
        {
            throw new NotImplementedException();
        }

        public int AddCust(string firstName, string lastName, string userName, string eMail, string passWord)
        {
            throw new NotImplementedException();
        }

        public bool CheckUnique(int mode, string check)
        {
            throw new NotImplementedException();
        }

        public void Edit(Customer cust)
        {
            throw new NotImplementedException();
        }

        public Customer FindByID(int custID)
        {
            return ctx.Customer.Find(custID);
        }
        public Customer FindByFirstName(string firstName)
        {
            IQueryable<Customer> cust = ctx.Customer.Where(u => u.FirstName == firstName);
            if (cust.Count() == 0)
            {
                return null;
            }
            return cust.First();
        }

        public IEnumerable<Customer> GetCusts()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetList()
        {
            throw new NotImplementedException();
        }

        public int NumberOfCustomers()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Search(int uChoice = 0, params string[] userSearch)
        {
            switch (uChoice)
            {
                case 1:

                    return ctx.Customer.Where(cust => cust.FirstName == userSearch[0] && cust.LastName == userSearch[1]);

                case 2:

                    return ctx.Customer.Where(cust => cust.UserName == userSearch[0]);

                default:

                    return ctx.Customer.Where(cust => cust.Email == userSearch[0]);
            }
        }

        public int ValidateCustomer(int id = -1, params string[] name)
        {
            throw new NotImplementedException();
        }

        public string VerifyCustomer(string username, out int id)
        {
            var cust = ctx.Customer.FirstOrDefault(cust => cust.UserName == username);
            if (cust == null)
            {
                id = -1;
            }
            else
            {
                id = cust.CustomerId;
                return cust.Password;
            }
            return ""; 
        }
    }
}
