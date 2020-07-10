using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using web_assignment_2020.Models;


namespace web_assignment_2020.DAL
{
    public class FlightRouteDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public FlightRouteDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "Air_Flights_DB_ConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        
        public List<int> GetRouteIDs()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT RouteID FROM FlightRoute ";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<int> routeIDs = new List<int>();
            while (reader.Read())
            {
                routeIDs.Add(reader.GetInt32(0));
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return routeIDs;
        }

        public List<int> GetAircraftIDs()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT AircraftID FROM Aircraft ";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<int> aircraftIDs = new List<int>();
            while (reader.Read())
            {
                aircraftIDs.Add(reader.GetInt32(0));
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return aircraftIDs;
        }
        
        public List<FlightScheduleViewModel> GetFlightSchedules(int routeId)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT * FROM FlightSchedule WHERE RouteID = @selectedRoute";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “branchNo”.
            cmd.Parameters.AddWithValue("@selectedRoute", routeId);

            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<FlightScheduleViewModel> flightSchedules = new List<FlightScheduleViewModel>();
            while (reader.Read())
            {
                flightSchedules.Add(
                    new FlightScheduleViewModel
                    {
                        ScheduleID = reader.GetInt32(0),
                        FlightNumber = reader.GetString(1),
                        RouteID = reader.GetInt32(2),
                        AircraftID = reader.GetInt32(3),
                        DepartureDateTime = reader.GetDateTime(4),
                        ArrivalDateTime = reader.GetDateTime(5),
                        EconomyClassPrice = reader.GetDecimal(6),
                        BusinessClassPrice = reader.GetDecimal(7),
                        Status = reader.GetString(8)

                    });
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return flightSchedules;

        }
        public int Add(FlightRoute flightRoute)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO FlightRoute (DepartureCity, DepartureCountry, ArrivalCity,
            ArrivalCountry, FlightDuration)
            OUTPUT INSERTED.RouteID
            VALUES(@departureCity, @departureCountry,@arrivalCity, @arrivalCountry,
            @flightDuration)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@departureCity", flightRoute.DepartureCity);
            cmd.Parameters.AddWithValue("@departureCountry", flightRoute.DepartureCountry);
            cmd.Parameters.AddWithValue("@arrivalCity", flightRoute.ArrivalCity);
            cmd.Parameters.AddWithValue("@arrivalCountry", flightRoute.ArrivalCountry);
            cmd.Parameters.AddWithValue("@flightDuration", flightRoute.FlightDuration);
            
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            flightRoute.RouteID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return flightRoute.RouteID;
        }
        public List<FlightRoute> GetAllFlightRoutes()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM FlightRoute ORDER BY RouteID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<FlightRoute> flightRoutes = new List<FlightRoute>();
            while (reader.Read())
            {
                flightRoutes.Add(
                new FlightRoute
                {
                    RouteID = reader.GetInt32(0), 
                    DepartureCity = reader.GetString(1), 
                    DepartureCountry = reader.GetString(2),
                    ArrivalCity = reader.GetString(3), 
                    ArrivalCountry = reader.GetString(4), 
                    FlightDuration = reader.GetInt32(5) 
                });
            }
           
             //Close DataReader
             reader.Close();
            //Close the database connection
            conn.Close();
            return flightRoutes;
        }
        public FlightRoute GetDetails(int routeID)
        {
            FlightRoute flightRoute = new FlightRoute();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM FlightRoute WHERE RouteID = @selectRouteID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectRouteID", routeID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader
                    flightRoute.RouteID = routeID;
                    flightRoute.DepartureCity = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    flightRoute.DepartureCountry = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    flightRoute.ArrivalCity = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    flightRoute.ArrivalCountry = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    flightRoute.FlightDuration = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;

                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return flightRoute;
        }

    }
    
}
