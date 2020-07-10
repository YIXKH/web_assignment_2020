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
    public class BookingDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public BookingDAL()
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
        public List<int> GetBookingIDs()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT BookingID FROM Booking ";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<int> bookingIDs = new List<int>();
            while (reader.Read())
            {
                bookingIDs.Add(reader.GetInt32(0));
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return bookingIDs;
        }
        public int Add(Booking booking)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Booking (CustomerID, ScheduleID, PassengerName, PassportNumber, Nationality,
            SeatClass, AmtPayable, Remarks, DateTimeCreated)
            OUTPUT INSERTED.BookingID
            VALUES(@CustomerID,@ScheduleID,@PassengerName,@PassportNumber,@Nationality,@SeatClass,
            @AmtPayable,@Remarks,@DateTimeCreated)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
            cmd.Parameters.AddWithValue("@ScheduleID", booking.ScheduleID);
            cmd.Parameters.AddWithValue("@PassengerName", booking.PassengerName);
            cmd.Parameters.AddWithValue("@PassportNumber", booking.PassportNumber);
            cmd.Parameters.AddWithValue("@Nationality", booking.Nationality);
            cmd.Parameters.AddWithValue("@SeatClass", booking.SeatClass);
            cmd.Parameters.AddWithValue("@AmtPayable", booking.AmtPayable);
            cmd.Parameters.AddWithValue("@Remarks", booking.Remarks);
            cmd.Parameters.AddWithValue("@DateTimeCreated", booking.DateTimeCreated);

            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            booking.BookingID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return booking.BookingID;
        }
        

        
    }
}
