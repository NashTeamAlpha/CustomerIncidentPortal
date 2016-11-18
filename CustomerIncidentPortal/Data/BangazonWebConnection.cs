using System;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Data
{
    //Class Name: BangazonWebConnection
    //Author: Delaine Wendling, Grant Regnier, Debbie Bourne, Chris Smalley, Zack Repass, Jamie Duke
    //Purpose of the class: The purpose of this class is to manage the methods that will pull information from the old database.
    //Methods in Class: insert(), execute()

    public class BangazonWebConnection
    {
        private string _connectionString = $"Data Source = {Environment.GetEnvironmentVariable("NTABangazonWeb_Db_Path")}";

        //Method Name: insert
        //Purpose of the Method: This method is the used to insert information into the database.  Currently not in use.
        //Arguments in Method: query

        public void insert(string query)
        {
            SqliteConnection dbcon = new SqliteConnection(_connectionString);
            
            dbcon.Open();
            SqliteCommand dbcmd = dbcon.CreateCommand();

            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();

            // clean up
            dbcmd.Dispose();
            dbcon.Close();
        }

        //Method Name: execute
        //Purpose of the Method: This method queries the database for the desired information and stages it to be formatted by the factory.
        //Arguments in Method: query, SqliteDataReader handler

        public void execute(string query, Action<SqliteDataReader> handler)
        {

            SqliteConnection dbcon = new SqliteConnection(_connectionString);

            dbcon.Open();
            SqliteCommand dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = query;

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
