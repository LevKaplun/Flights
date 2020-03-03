using Flights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Flights.Controllers
{
    public class FlightController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Flight> Get()
        {
            Flight.GetMyFlights();
            return Flight.flightList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        [Route("api/Flight/{city}")]
        // GET api/car/discounted/2017
        public IEnumerable<Flight> GetByCity(string city)
        {
            return Flight.GetFlightByRCity(city);
        }

        [HttpPost]
        [Route("api/Flight/{oOrf}")]
        public void Post(string oOrf, [FromBody]Flight f)
        {
            if (oOrf == "f")
            {
                Flight.flightList.Add(f);
                Flight.AddFlightToDB(f);
            }
            else
            {
                Flight.AddOFlightToDB(f);
            }
        }


        // PUT api/<controller>/5
        [HttpGet]
        [Route("api/Flight/Order")]
        public List<Flight> GetOrder()
        {
            return Flight.GetOFlights();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}