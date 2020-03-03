using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flights.Models.DAL;

namespace Flights.Models
{
    public class Flight
    {
        string fId;
        string path;
        DateTime from;
        DateTime to;
        double price;
        List<string> routeCnames;
        static public List<Flight> flightList = new List<Flight>();

        public string Path { get => path; set => path = value; }
        public DateTime From { get => from; set => from = value; }
        public DateTime To { get => to; set => to = value; }
        public double Price { get => price; set => price = value; }
        public List<string> RouteCnames { get => routeCnames; set => routeCnames = value; }
        public string FId { get => fId; set => fId = value; }

        public Flight(string fId,string path, DateTime from, DateTime to, double price, List<string> route)
        {
            FId = fId;
            Path = path;
            From = from;
            To = to;
            Price = price;
            RouteCnames = route;
        }

        public static void AddFlightToDB(Flight flight)
        {
            DBservices dbs = new DBservices();
            dbs.insertF(flight);
        }
        public static void AddOFlightToDB(Flight flight)
        {
            DBservices dbs = new DBservices();
            dbs.insertFO(flight);
        }
        public static void GetMyFlights()
        {
            DBservices dbs = new DBservices();
            flightList = dbs.GetMyFlights();
        }
        public static List<Flight> GetOFlights()
        {
            DBservices dbs = new DBservices();
            return dbs.GetOFlights();
        }

        public static List<Flight> GetFlightByRCity(string city)
        {
            List<Flight> listNew = new List<Flight>();
            for (int i = 0; i < flightList.Count; i++)
            {
                for (int j = 0; j < flightList[i].RouteCnames.Count; j++)
                {
                    if (flightList[i].RouteCnames[j] == city)
                        listNew.Add(flightList[i]);
                }
                
            }
            return listNew;
        }
    }
}