using System;
using System.Collections.Generic;
using System.Linq;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    //Class Name: CreateIncident
    //Author: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class: The purpose of this class is to display the series of prompts that will allow am employee to create an incident and save it to the database
    //Methods in Class: Action(), displayOrders(), displayIncidentType()
    public class CreateIncident
    {
        //Method Name: Action
        //Purpose of the Method: This method shows the prompts that ask the employee to type in the first and last name of the customer for whom the incident will be created. If there is more than one customer with that first and last name in the database the employee will be prompted to select the customer based on customer id. Once the customer has been selected, that customer's first and last name are added to the active incident singleton.
        //Arguments in Method: No arguments passed into this method 
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
        //Method Name: displayOrders
        //Purpose of the Method: The purpose of this method is to take the customerId of the selected customer and display the orders, in the console, related to that customer. The employee is then prompted to select an order. Once the order has been selected, that order id is added to the active incident singleton.
        //Arguments in Method: The customerId of the selected customer.
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
        //Method Name: displayIncidentTypes
        //Purpose of the Method: The purpose of this method is to grab all incident types from the database and display them on the screen. The employee can then select an incident type appropriate to the incident he/she is creating. Once an incident type has been selected, its id is added to the active incident singleton. The Employee id is also added by grabbing the active Employee id and the incident is saved to the database without a resolution. The employee is redirected to the incident details view.
        //Arguments in Method: No arguments are passed into this method.
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
                    IncidentDetail.Action();
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
