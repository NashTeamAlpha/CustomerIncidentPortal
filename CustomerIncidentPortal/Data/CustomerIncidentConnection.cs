using System;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Data
{
    public class CustomerIncidentConnection
    {
        private string _connectionString = System.Environment.GetEnvironmentVariable("NTABangazon_Incident_Db_Path");

        public void insert(string query)
        //This method executes every time you need to add something to the database.
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

        public void execute(string query, Action<SqliteDataReader> handler)
        {
            //Set up connecction
            SqliteConnection dbcon = new SqliteConnection(_connectionString);
            //Open connecction
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
