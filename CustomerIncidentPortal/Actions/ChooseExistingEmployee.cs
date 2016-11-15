using System;
using System.Linq;
using System.Collections.Generic;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    class ChooseExistingEmployee
    {
        public static void Action ()
        {
            EmployeeFactory employeeFactory = new EmployeeFactory();

            Banner.Action();

            Console.WriteLine("Welcome Employee, enter your first and last name to log in");
            string EmployeeName = Console.ReadLine();

            string[] EmployeeNameArray = EmployeeName.Split(new char[] { ' ' });

            string EmployeeFirstName = EmployeeNameArray[0];
            string EmployeeLastName = EmployeeNameArray[1];

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

                int Choice = Int32.Parse(Console.ReadLine());

                try
                {
                    Employee SelectedEmployee = ListOfEmployees.Where(e => e.EmployeeId == Choice).Single();

                    employeeFactory.ActiveEmployee = SelectedEmployee;
                }

                catch
                {
                    Console.WriteLine("\r\nThat was not one of the options\r\n");
                    Action();
                }
            }

            Console.ReadLine(); //Replace with MainMenu.Action() After it's been created.
        }
    }
}
