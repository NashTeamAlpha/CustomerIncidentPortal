using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    public class CreateIncident
    {
        public static void Action()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            incidentFactory.ActiveIncident = new Incident();
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
                    int chosenCustomerId = Convert.ToInt32(Console.ReadLine());
                    Customer SelectedCustomer = customerList.Where(c => c.CustomerId == chosenCustomerId).Single();
                    incidentFactory.ActiveIncident.CustomerFirstName = CustomerFirstName;
                    incidentFactory.ActiveIncident.CustomerLastName = CustomerLastName;
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
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            CustomerFactory customerFactory = new CustomerFactory();
            List<Order> listOfOrders = customerFactory.GetOrders(CustomerId);
            if (listOfOrders.Count() == 0)
            {
                Console.WriteLine("\nThere are no orders for this customer.\n");
                Action();
            }
            else
            {
                Console.WriteLine("Type in the ID of the order for which you would like to create an incident");
                for (var i = 0; i < listOfOrders.Count(); i++)
                {
                    Console.WriteLine($"ID: {listOfOrders[i].OrderId} Date Completed: {listOfOrders[i].DateCompleted}");
                }
                try
                {
                    int chosenOrderId = Int32.Parse(Console.ReadLine());
                    Order SelectedOrder = listOfOrders.Where(o => o.OrderId == chosenOrderId).Single();
                    incidentFactory.ActiveIncident.OrderId = SelectedOrder.OrderId;
                    displayIncidentTypes();
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid order\n");
                    Action();
                }
            }
        }
        public static void displayIncidentTypes()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            List<IncidentType> listOfIncidents = incidentFactory.GetIncidentTypes();
            if (listOfIncidents.Count() == 0)
            {
                Console.WriteLine("\nSorry, there was a problem with the database. Please try again.\n");
                Action();
            }
            else
            {
                Console.WriteLine("Type in the number representing the incident type you wish to add.");
                for (var i = 0; i < listOfIncidents.Count(); i++)
                {
                    Console.WriteLine($"{listOfIncidents[i].IncidentTypeId}. {listOfIncidents[i].IncidentTypeName}");
                }
                try
                {
                    int chosenIncidentTypeId = Int32.Parse(Console.ReadLine());
                    IncidentType SelectedIncidentType = listOfIncidents.Where(o => o.IncidentTypeId == chosenIncidentTypeId).Single();
                    incidentFactory.ActiveIncident.IncidentTypeId = SelectedIncidentType.IncidentTypeId;
                    incidentFactory.ActiveIncident.IsResolved = "false";
                    incidentFactory.ActiveIncident.Resolution = "";
                    incidentFactory.ActiveIncident.EmployeeId = employeeFactory.ActiveEmployee.EmployeeId;
                    incidentFactory.ActiveIncident.Save();
                    //Should call the incident detail action
                    Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid incident type\n");
                    Action();
                }
            }
        }
    }
}
