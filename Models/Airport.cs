using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flights.Models.DAL;

namespace Flights.Models
{
    public class Airport
    {
        string airport_Id;
        string name;
        double lat;
        double lon;

        public Airport()
        {

        }
        public Airport(string airport_Id, string name, double lat, double lon)
        {
            Airport_Id = airport_Id;
            Name = name;
            Lat = lat;
            Lon = lon;
        }

        public string Airport_Id { get => airport_Id; set => airport_Id = value; }
        public string Name { get => name; set => name = value; }
        public double Lat { get => lat; set => lat = value; }
        public double Lon { get => lon; set => lon = value; }
        public List<Airport> GetAirports()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAirports();
        }
    }
}