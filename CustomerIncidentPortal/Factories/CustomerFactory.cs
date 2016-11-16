using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Factories
{
    public class CustomerFactory
    {
        public List<Customer> GetCustomer(Customer customer)
        {
            BangazonWebConnection Conn = new BangazonWebConnection();
            List<Customer> customerList = new List<Customer>() ;

            string query = $"select CustomerId, FirstName, LastName from Customer Where '{customer.FirstName} = Customer.FirstName' And '{customer.LastName} = Customer.LastName'";
            Conn.execute(query, (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    customerList.Add(new Customer
                    {
                        CustomerId = reader.GetInt32(0),
                        FirstName = reader[1].ToString(),
                        LastName = reader[2].ToString()
                    });
                }
            });
            return customerList;
        }
    }
}
