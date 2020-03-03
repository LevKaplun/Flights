using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Flights.Models;

namespace Flights.Controllers
{
    public class AirportsController : ApiController
    {
        // GET: api/Airports
        public List<Airport> Get()
        {
            Airport a = new Airport();
            return a.GetAirports();
        }

        // GET: api/Airports/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Airports
        public void Post([FromBody]Airports airports)
        {
            //airports.insertA();
        }

        // PUT: api/Airports/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Airports/5
        public void Delete(int id)
        {
        }
    }
}
