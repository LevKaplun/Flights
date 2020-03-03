using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flights.Models.DAL;

namespace Flights.Models
{
    public class Admin
    {
        string email;
        string password;

        public Admin(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public Admin()
        {

        }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public List<Admin> GetAdmins()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAdmins();
        }
    }
}