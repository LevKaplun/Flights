using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Flights.Models;

namespace Flights.Controllers
{
    public class DiscountController : ApiController
    {
        // GET: api/Discount
        public IEnumerable<Discount> Get()
        {
            return Discount.GetDiscounts();
        }

        // GET: api/Discount/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Discount
        public void Post([FromBody]Discount d)
        {
            Discount.PostDiscount(d);
        }

        // PUT: api/Discount/5
        public void Put([FromBody]Discount d)
        {
            Discount.UpdateDiscountToDB(d);
        }

        // DELETE: api/Discount/5
        public void Delete(int id)
        {
        }
    }
}
