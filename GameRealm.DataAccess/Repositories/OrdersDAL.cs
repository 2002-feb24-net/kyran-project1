using GameRealm.DataAccess.Model;
using GameRealm.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GameRealm.Domain.Model;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameRealm.DataAccess
{
    public class OrdersDAL : IOrders
        //customer data access library
    {
        readonly Game_RealmContext ctx = new Game_RealmContext();

        public Orders FindByID(int id)
        {
            return ctx.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Store)
                    .Include("OrderItem.P")
                    .Include("OrderItem.P.P")
                    .FirstOrDefault(m => m.CustomerId == id);
        }

        public void Remove(int id)
        {
            try
            {
                var toRemove = ctx.Orders.Find(id);
                ctx.Remove(toRemove);
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return;
            }
        }

        public IEnumerable<Locations> GetLocs()
        {
            return ctx.Locations;
        }

        public IEnumerable<Orders> GetOrds()
        {
            return  ctx.Orders.Include(o => o.Customer).Include(o => o.Store).ToList();
        }

        /// <summary>
        /// Adds an order to database
        /// </summary>
        /// <param name="cust"></param>
        public int Add(Orders o)
        {
            ctx.Orders.Add(o);
/*            ctx.SaveChanges();*/
            ctx.Entry(o).Reload();
            return o.CustomerId;
        }

        /// <summary>
        /// Sets order's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Orders o)
        {
            ctx.Entry(o).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        //Returns price of added item
        public void AddOrderItem(Orderline item)
        {
            ctx.Orderline.Add(item);
            ctx.SaveChanges();
        }

        public int ValidateOrder(int id)
        {
            var ordList =  GetOrders();
            foreach (var order in ordList)
            {
                if (id > 0 && order.OrderId == id)
                {
                    return id;
                }

            }
            return -1;
        }

        public int CreateOrder(int cid, int lid)
        {
            var new_order = new Orders
            {
                CustomerId = cid,
                StoreId = lid,
                Checkout = 0,
                Time = DateTime.Now,
            };
            ctx.Orders.Add(new_order);
            ctx.SaveChanges();
            return new_order.CustomerId;
        }

        public List<Orders> GetOrders(int mode = 0, params string[] search_param)
        {
            var orderList = ctx.Orders.Include("Store").Include("Customer").AsQueryable();
            using (var context = new Game_RealmContext())
            {
                switch (mode)
                {
                    case 1:
                        orderList = orderList
                        .Where(o => o.Store.StoreName == search_param[0]);
                        break;
                    case 2:
                        orderList = orderList
                        .Where(o => o.Customer.FirstName == search_param[0] && o.Customer.LastName == search_param[1]);
                        break;
                    case 3:
                        orderList = orderList
                        .Where(o => o.CustomerId == Convert.ToInt32(search_param[0]));
                        break;
                    default:
                        break;
                }
            }
            return  orderList
                        .Include("OrderItem")
                        .Include("OrderItem.P")
                        .Include("OrderItem.P.P")
                        .ToList();
        }

        public Orders orderDet(int orderid)
        {
            return ctx.Orders
              .Include(o => o.Customer)
              .Include(o => o.Store)
              .Include(o => o.Orderline)
              .ThenInclude(or => or.Product)
              .FirstOrDefault(m => m.OrderId == orderid);
        }

        public Orders preOrder()
        {
            var lastOrder = ctx.Orders.Max(s => s.OrderId);
            return orderDet(lastOrder);
        }
    }
}
