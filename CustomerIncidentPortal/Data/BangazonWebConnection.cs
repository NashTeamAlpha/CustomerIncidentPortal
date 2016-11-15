using System;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Data
{
    public class BangazonWebConnection
    {
        private string _connectionString = $"Data Source = {Environment.GetEnvironmentVariable("NTABangazonWeb_DB_Path")}";

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
