using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flights.Models.DAL;

namespace Flights.Models
{
    public class Discount
    {
        int id;
        string from;
        string to;
        DateTime fromDate;
        DateTime toDate;
        int pDiscount;

        public Discount(int id, string from, string to, DateTime fromDate, DateTime toDate, int pDiscount)
        {
            Id = id;
            From = from;
            To = to;
            FromDate = fromDate;
            ToDate = toDate;
            PDiscount = pDiscount;
        }
        public int Id { get => id; set => id = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public DateTime FromDate { get => fromDate; set => fromDate = value; }
        public DateTime ToDate { get => toDate; set => toDate = value; }
        public int PDiscount { get => pDiscount; set => pDiscount = value; }
        public static void UpdateDiscountToDB(Discount d)
        {
            DBservices dbs = new DBservices();
            dbs.UpdateD(d);
        }
        public static List<Discount> GetDiscounts()
        {
            DBservices dbs = new DBservices();
            return dbs.GetDiscount();
        }
        public static void PostDiscount(Discount d)
        {
            DBservices dbs = new DBservices();
            dbs.PostD(d);
        }
    }
}