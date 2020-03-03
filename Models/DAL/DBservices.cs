using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

namespace Flights.Models.DAL
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices()
        {
        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }
        public SqlConnection connect(String conString)
        {
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //Dicount
        public List<Discount> GetDiscount()
        {
            List<Discount> listD = new List<Discount>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                string selectSTR = "SELECT * FROM MyFlights_Discount";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Discount d = new Discount(Convert.ToInt32(dr["Id"]), (string)dr["From"], (string)dr["To"], Convert.ToDateTime(dr["FromDate"]), Convert.ToDateTime(dr["ToDate"]), Convert.ToInt32(dr["Discount"]));

                    listD.Add(d);
                }

                return listD;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        public void UpdateD(Discount d)
        {

            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = $"UPDATE MyFlights_Discount SET [From]='{d.From}',[To]='{d.To}',FromDate='{d.FromDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}',ToDate='{d.ToDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}',Discount={d.PDiscount} WHERE Id = {d.Id}";      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }


        }
        public void PostD(Discount d)
        {

            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = $"INSERT INTO MyFlights_Discount ([From] ,[To] ,[FromDate],[ToDate] ,[Discount]) VALUES ('{d.From}','{d.To}','{d.FromDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{d.ToDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}',{d.PDiscount})";      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }


        }



        public void insertO(List<Order> listO)
        {
            foreach (var o in listO)
            {
                SqlConnection con;
                SqlCommand cmd;
                try
                {
                    con = connect("DBConnectionString"); // create the connection
                }
                catch (Exception ex)
                {
                    // write to log
                    throw (ex);
                }

                string cStr = BuildOrderInsertCommand(o);      // helper method to build the insert string

                cmd = CreateCommand(cStr, con);             // create the command

                try
                {
                    cmd.ExecuteNonQuery(); // execute the command
                }
                catch (Exception ex)
                {
                    // write to log
                    throw (ex);
                }

                finally
                {
                    if (con != null)
                    {
                        // close the db connection
                        con.Close();
                    }
                }
            }

        }
        private string BuildOrderInsertCommand(Order o)
        {
            String command;
            StringBuilder sb = new StringBuilder();

            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}' ,'{2}')", o.Email, o.Name, o.FId);
            String prefix = "INSERT INTO MyFlights_Orders_2020 " + "(Email, Name, FId) ";
            command = prefix + sb.ToString();

            return command;
        }
        public List<Order> GetMyOrders()
        {
            List<Order> listO = new List<Order>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                string selectSTR = "SELECT * FROM MyFlights_Orders_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Order o = new Order((string)dr["Email"], (string)dr["Name"], (string)dr["FId"]);

                    listO.Add(o);
                }

                return listO;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        public int insertF(Flight f)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = FlightBuildInsertCommand(f);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private string FlightBuildInsertCommand(Flight f)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Values('{0}','{1}','{2}',{3},'{4}')", f.FId, f.From.ToString("yyyy-MM-dd HH:mm:ss.fff"), f.To.ToString("yyyy-MM-dd HH:mm:ss.fff"), f.Price, f.Path);
            String prefix = "INSERT INTO MyFlights_2020 (FId,From_Time,To_Time,Price,path) ";
            command = prefix + sb.ToString();
            foreach (var item in f.RouteCnames)
            {
                sb.Clear();
                sb.AppendFormat("Values('{0}','{1}')", f.FId, item);
                command += " INSERT INTO My_Flights_Routes_2020 (FId,City_Name)" + sb.ToString();

            }


            return command;
        }

        public int insertFO(Flight f)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = OFlightBuildInsertCommand(f);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private string OFlightBuildInsertCommand(Flight f)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Values('{0}','{1}','{2}',{3},'{4}')", f.FId, f.From.ToString("yyyy-MM-dd HH:mm:ss.fff"), f.To.ToString("yyyy-MM-dd HH:mm:ss.fff"), f.Price, f.Path);
            String prefix = "INSERT INTO MyFlights_Flights_Orderd_2020 (FId,From_Time,To_Time,Price,path) ";
            command = prefix + sb.ToString();
            foreach (var item in f.RouteCnames)
            {
                sb.Clear();
                sb.AppendFormat("Values('{0}','{1}')", f.FId, item);
                command += " INSERT INTO My_Flights_Order_Routes_2020 (FId,City_Name)" + sb.ToString();

            }


            return command;
        }
        public List<Flight> GetOFlights()
        {
            List<Flight> MF = new List<Flight>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM MyFlights_Flights_Orderd_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Flight f = new Flight((string)dr["FId"], (string)dr["path"], Convert.ToDateTime(dr["From_Time"]), Convert.ToDateTime(dr["To_Time"]), Convert.ToInt32(dr["Price"]), GetORoutes((string)dr["FId"]));

                    MF.Add(f);
                }

                return MF;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        private List<string> GetORoutes(string fid)
        {
            List<string> MR = new List<string>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = $"SELECT * FROM MyFlights_Flights_Orderd_2020 T inner join My_Flights_Order_Routes_2020 F on T.FId=F.FId where T.FId='{fid}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    string f = (string)dr["City_Name"];

                    MR.Add(f);
                }

                return MR;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }



        //public int insertAirport(Airport a)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("DBConnectionString");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    String cStr = APBuildInsertCommand(a);

        //    cmd = CreateCommand(cStr, con);

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery();
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }

        //}
        //private string APBuildInsertCommand(Airport a)
        //{
        //    String command;

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("Values('{0}','{1}','{2}','{3}')", a.Airport_Id, a.Name, a.Lat, a.Lon);
        //    String prefix = "INSERT INTO Airports_2020 (Airport_Id,Name,Lat,Lon) ";
        //    command = prefix + sb.ToString();

        //    return command;
        //}

        public List<Airport> GetAirports()
        {
            List<Airport> AP = new List<Airport>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM Airports_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Airport a = new Airport((string)dr["Airport_Id"], (string)dr["Name"], Convert.ToDouble(dr["Lat"]), Convert.ToDouble(dr["Lon"]));

                    AP.Add(a);
                }

                return AP;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> Admins = new List<Admin>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM MyFlights_AdminLogin_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Admin a = new Admin((string)dr["Email"], (string)dr["Password"]);

                    Admins.Add(a);
                }

                return Admins;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        public List<Flight> GetMyFlights()
        {
            List<Flight> MF = new List<Flight>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM MyFlights_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Flight f = new Flight((string)dr["FId"], (string)dr["path"], Convert.ToDateTime(dr["From_Time"]), Convert.ToDateTime(dr["To_Time"]), Convert.ToInt32(dr["Price"]), GetRoutes((string)dr["FId"]));

                    MF.Add(f);
                }

                return MF;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        private List<string> GetRoutes(string fid)
        {
            List<string> MR = new List<string>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = $"SELECT * FROM MyFlights_2020 T inner join My_Flights_Routes_2020 F on T.FId=F.FId where T.FId='{fid}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    string f = (string)dr["City_Name"];

                    MR.Add(f);
                }

                return MR;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //public List<User> getUnFavoritesUsers()
        //{
        //    List<User> users = new List<User>();
        //    SqlConnection con = null;

        //    try
        //    {
        //        con = connect("DBConnectionString");
        //        String selectSTR = "SELECT * FROM Tinder_2020 T where T.id != ALL(select * from Tinder_2020_F)";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);

        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (dr.Read())
        //        {
        //            User u = new User();
        //            u.Id = Convert.ToInt32(dr["id"]);
        //            u.Name = (string)dr["name"];
        //            u.Gender = (string)dr["gender"];
        //            u.Age = Convert.ToInt32(dr["age"]);
        //            u.Height = Convert.ToDouble(dr["height"]);
        //            u.City = (string)(dr["city"]);
        //            u.Hobbies = ((string)dr["hobbies"]).Split(',');
        //            u.Image = (string)(dr["image"]);
        //            u.Premium = (bool)(dr["premium"]);

        //            users.Add(u);
        //        }

        //        return users;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }

        //    }
        //}
        //public int DeleteF(int id)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("DBConnectionString");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    String cStr = "delete from Tinder_2020_F where id = " + id;

        //    cmd = CreateCommand(cStr, con);

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery();
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }

        //}


        //public DBservices readCars()
        //{
        //    SqlConnection con = null;
        //    try
        //    {
        //        con = connect("DBConnectionString");
        //        da = new SqlDataAdapter("select * from cars_2020", con);
        //        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        dt = ds.Tables[0];
        //    }

        //    catch (Exception ex)
        //    {
        //        // write errors to log file
        //        // try to handle the error
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }


        //    return this;

        //}

        public void update()
        {
            da.Update(dt);
        }

    }

}