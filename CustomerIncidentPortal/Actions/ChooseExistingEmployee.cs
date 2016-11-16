using System;
using System.Linq;
using System.Collections.Generic;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    public class ChooseExistingEmployee
    {
        public static void Action()
        {

            EmployeeFactory employeeFactory = EmployeeFactory.Instance;

            Banner.Action();

            Console.WriteLine("Welcome Employee, enter your first and last name to log in\r\nOr enter 'new user' to make a new account");

            string EmployeeName = Console.ReadLine();

            string[] EmployeeNameArray = EmployeeName.Split(new char[] { ' ' });

            string EmployeeFirstName = null;
            string EmployeeLastName = null;

            try
            {
                EmployeeFirstName = EmployeeNameArray[0];
                EmployeeLastName = EmployeeNameArray[1];
            }
            catch
            {
                Console.WriteLine("\r\nInvalid Entry\r\n");
                Action();
            }
            if (EmployeeFirstName == "new" && EmployeeLastName == "user")
            {
                NewUser.Action();
            }
            List<Employee> ListOfEmployees = employeeFactory.GetEmployeeByName(EmployeeFirstName, EmployeeLastName);

            if(ListOfEmployees.Count == 0)
            {
                Console.WriteLine(" \r\nUser Not Found\r\n");
                Action();
            }

            if(ListOfEmployees.Count == 1)
            {
                employeeFactory.ActiveEmployee = ListOfEmployees[0];
            }
            else
            {
                Console.WriteLine($"There is more than one {EmployeeFirstName} {EmployeeLastName} \r\nEnter Your Employee ID and Press Enter\r\n");
                
                foreach(Employee employee in ListOfEmployees)
                {
                    Console.WriteLine($"ID: {employee.EmployeeId} {employee.FirstName} {employee.LastName} Start Date: {employee.StartDate}"); 
                }
           
                try
                {
                    int Choice = Int32.Parse(Console.ReadLine());

                    Employee SelectedEmployee = ListOfEmployees.Where(e => e.EmployeeId == Choice).Single();

                    employeeFactory.ActiveEmployee = SelectedEmployee;
                }

                catch
                {
                    Console.WriteLine("\r\nThat was not one of the options\r\n");
                    Action();
                }
            }
            MainMenu.Action();
        }
    }
}
