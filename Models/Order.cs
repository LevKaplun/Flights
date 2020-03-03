using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flights.Models.DAL;

namespace Flights.Models
{
    public class Order
    {
        string email;
        string name;
        string fId;

        public Order()
        {

        }
        public Order(string email, string name, string fId)
        {
            Email = email;
            Name = name;
            FId = fId;
        }

        public string Email { get => email; set => email = value; }
        public string Name { get => name; set => name = value; }
        public string FId { get => fId; set => fId = value; }

        public static void AddOrderToDB(List<Order> listO)
        {
            DBservices dbs = new DBservices();
            dbs.insertO(listO);
        }
        public static List<Order> GetMyOrders()
        {
            DBservices dbs = new DBservices();
            return dbs.GetMyOrders();
        }

    }
}