using System;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Data
{
    public class CustomerIncidentConnection
    {
        //Class Name: Incident Report
        //Author: Delaine, Chris, Jamie
        //Purpose of the class: The purpose of this class is to manage the methods that will use the Environment Variable to connect the data in Sqlite db to the factories.
        //Methods in Class: Insert(), Execute()
        private string _connectionString = $"Data Source = {Environment.GetEnvironmentVariable("NTABangazon_Incident_Db_Path")}";

        public void insert(string query)
        //Method Name: Insert
        //Purpose of the Method: //This method executes every time you need to insert something to the Customer Incident database.
        //Arguments in Method: No arguments passed to this method 

        {
            SqliteConnection dbcon = new SqliteConnection(_connectionString);

            //Open the connection
            dbcon.Open();
            //Create a command
            SqliteCommand dbcmd = dbcon.CreateCommand();
            //Establish a what SQL command will be established
            dbcmd.CommandText = query;
            //Execute command.
            dbcmd.ExecuteNonQuery();
            // clean up
            dbcmd.Dispose();
            dbcon.Close();
        }
        //Method Name: Execute
        //Purpose of the Method: //This method executes every time you need to insert something to the Customer Incident database.
        //Arguments in Method: No arguments passed to this method 
        public void execute(string query, Action<SqliteDataReader> handler)
        {
            //Set up connection
            SqliteConnection dbcon = new SqliteConnection(_connectionString);
            //Open connection
            dbcon.Open();
            //Establish command
            SqliteCommand dbcmd = dbcon.CreateCommand();
            //
            dbcmd.CommandText = query;
            //
            using (var reader = dbcmd.ExecuteReader())
            {
                handler(reader);
            }

            // clean up
            dbcmd.Dispose();
            dbcon.Close();
        }
    }
}
