using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    public class CreateIncidentAction
    {
        public static void Action()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            Banner.Action();
            Console.WriteLine("Enter the Customer First Name and Last Name");
            string customer = Console.ReadLine();
            string CustomerFirstName = null;
            string CustomerLastName = null;
            try
            {
                string[] CustomerNameArr = customer.Split(new char[] {' '});
                CustomerFirstName = CustomerNameArr[0];
                CustomerLastName = CustomerNameArr[1];
            }
            catch
            {
                Console.WriteLine("\nInvalid Entry\n");
                Action();
            }
            CustomerFactory customerFactory = new CustomerFactory();
            Customer createdCustomer = new Customer();
            createdCustomer.FirstName = CustomerFirstName;
            createdCustomer.LastName = CustomerLastName;
            List<Customer> customerList = customerFactory.GetCustomer(createdCustomer);
            if (customerList.Count() == 0)
            {
                Console.WriteLine("\nThat Customer is not in the database. Make sure that the first letter of the first and last name are capitalized.\n");
                Action();
            }
            else if (customerList.Count() == 1)
            {
                Console.WriteLine(customerList.Count());
                incidentFactory.ActiveIncident.CustomerFirstName = customerList[0].FirstName;
                incidentFactory.ActiveIncident.CustomerLastName = customerList[0].LastName;
                displayOrders(customerList[0].CustomerId);
            }
            else
            {
                Console.WriteLine("Type in the ID of the customer you would like to choose and press enter");
                for (var i = 0; i < customerList.Count(); i++)
                {
                    Console.WriteLine($"ID: {customerList[i].CustomerId} Name: {customerList[i].LastName}, {customerList[i].FirstName}");
                }
                try
                {
                    int chosenCustomerId = Int32.Parse(Console.ReadLine());
                    Customer SelectedCustomer = customerList.Where(c => c.CustomerId == chosenCustomerId).Single();
                    incidentFactory.ActiveIncident.CustomerFirstName = SelectedCustomer.FirstName;
                    incidentFactory.ActiveIncident.CustomerLastName = SelectedCustomer.LastName;
                    displayOrders(SelectedCustomer.CustomerId);
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid customer\n");
                    Action();
                }
            }
        }
        public static void displayOrders(int CustomerId)
        {
            
        }
    }
}
