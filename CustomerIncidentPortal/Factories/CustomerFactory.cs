using System.Collections.Generic;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Factories
{
    //Class Name: CustomerFactory
    //Author: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class: The purpose of this class is to create methods that allow other parts of the program to get customer information from the database.
    //Methods in Class: GetCustomer(), GetOrders()
    public class CustomerFactory
    {
        //Method Name: GetCustomer
        //Purpose of the Method: This method takes in a customer and returns a List of Customers that have the same first and last name as the customer passed in. This is utilized when the employee types in a customer first and last name and needs to receive the customer with that first and last name back.
        //Arguments in Method: Customer customer. 
        public List<Customer> GetCustomer(Customer customer)
        {
            BangazonWebConnection Conn = new BangazonWebConnection();
            List<Customer> customerList = new List<Customer>() ;

            string query = $"select CustomerId, FirstName, LastName from Customer Where '{customer.FirstName}' = Customer.FirstName And '{customer.LastName}' = Customer.LastName";
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
                reader.Close();
            });
            return customerList;
        }
        //Method Name: GetOrders
        //Purpose of the Method: This method takes in a customerId and returns a list of Orders related to that customerId. This is utilized when an incident is being created and the employee needs to select the order for which the incident will be created.
        //Arguments in Method: int CustomerId. 
        public List<Order> GetOrders(int customerId)
        {
            BangazonWebConnection Conn = new BangazonWebConnection();
            List<Order> orderList = new List<Order>();

            string query = $"select OrderId, DateCompleted, CustomerId from 'Order' Where '{customerId}' = CustomerId";
            Conn.execute(query, (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    orderList.Add(new Order
                    {
                        OrderId = reader.GetInt32(0),
                        DateCompleted = reader[1].ToString(),
                        CustomerId = reader.GetInt32(2)
                    });
                }
                reader.Close();
            });
            return orderList;
        }
    }
}
