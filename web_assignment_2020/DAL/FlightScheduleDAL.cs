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
    public class FlightScheduleDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public FlightScheduleDAL()
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
        
        public int Add(FlightSchedule flightSchedule, int flightDuration)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO FlightSchedule(FlightNumber, RouteID,
            AircraftID, DepartureDateTime, ArrivalDateTime, EconomyClassPrice, BusinessClassPrice, Status)
            OUTPUT INSERTED.ScheduleID
            VALUES(@flightNumber, @routeID, @aircraftID, @departureDateTime,
            @arrivalDateTime, @economyClassPrice, @businessClassPrice, @status)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@flightNumber", flightSchedule.FlightNumber);
            cmd.Parameters.AddWithValue("@routeID", flightSchedule.RouteID);
            cmd.Parameters.AddWithValue("@aircraftID", flightSchedule.AircraftID);
            cmd.Parameters.AddWithValue("@departureDateTime", flightSchedule.DepartureDateTime);
            cmd.Parameters.AddWithValue("@arrivalDateTime", flightSchedule.DepartureDateTime.AddHours(flightDuration));
            cmd.Parameters.AddWithValue("@economyClassPrice", flightSchedule.EconomyClassPrice);
            cmd.Parameters.AddWithValue("@businessClassPrice", flightSchedule.BusinessClassPrice);
            cmd.Parameters.AddWithValue("@status", flightSchedule.Status);


            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            flightSchedule.ScheduleID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return flightSchedule.ScheduleID;
        }
        public FlightSchedule GetDetails(int scheduleID)
        {
            FlightSchedule flightSchedule = new FlightSchedule();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM FlightSchedule WHERE ScheduleID = @selectScheduleID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectScheduleID", scheduleID);
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
                    flightSchedule.ScheduleID = reader.GetInt32(0);
                    flightSchedule.FlightNumber = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    flightSchedule.RouteID = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    flightSchedule.AircraftID = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    flightSchedule.DepartureDateTime = reader.GetDateTime(4);
                    flightSchedule.ArrivalDateTime = reader.GetDateTime(5);
                    flightSchedule.EconomyClassPrice = !reader.IsDBNull(6) ? reader.GetDecimal(6) : 0;
                    flightSchedule.BusinessClassPrice = !reader.IsDBNull(7) ? reader.GetDecimal(7) : 0;
                    flightSchedule.Status = !reader.IsDBNull(8) ? reader.GetString(8) : null;
                }
            };
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return flightSchedule;
        }
        public void Update(int? id, string status)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE FlightSchedule SET Status= @selectedStatus WHERE ScheduleID = @selectedScheduleID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@selectedStatus", status);
            cmd.Parameters.AddWithValue("@selectedScheduleID", id.Value);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
           cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
        }
        public List<FlightSchedule> GetAllFlightSchedule()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM FlightSchedule ORDER BY ScheduleID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<FlightSchedule> flightSchedules = new List<FlightSchedule>();
            while (reader.Read())
            {
                flightSchedules.Add(
                new FlightSchedule
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
            //Close the database connection
            conn.Close();
            return flightSchedules;
        }
        public List<BookingViewModel> GetBooking(int scheduleId)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT * FROM Booking WHERE ScheduleID = @selectedScheduleID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “branchNo”.
            cmd.Parameters.AddWithValue("@selectedScheduleID", scheduleId);

            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<BookingViewModel> bookings = new List<BookingViewModel>();
            while (reader.Read())
            {
                bookings.Add(
                    new BookingViewModel
                    {
                        BookingID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        ScheduleID = reader.GetInt32(2),
                        PassengerName = reader.GetString(3),
                        PassportNumber = reader.GetString(4),
                        Nationality = reader.GetString(5),
                        SeatClass = reader.GetString(6),
                        AmtPayable = reader.GetDecimal(7),
                        Remarks = !reader.IsDBNull(8)?reader.GetString(8):null,
                        DateTimeCreated =  reader.GetDateTime(9)

                    });
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return bookings;

        }
    }
}
