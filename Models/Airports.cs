using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flights.Models.DAL;

namespace Flights.Models
{
    public class Airports
    {
        List<Airport> aports;

        
        public Airports(List<Airport> airports)
        {
            Aports = airports;
        }

        public List<Airport> Aports { get => aports; set => aports = value; }

        //public void insertA()
        //{
        //    DBservices dbs = new DBservices();
        //    foreach (var item in aports)
        //    {
        //        dbs.insertAirport(item);
        //    }

        //}
    }
}