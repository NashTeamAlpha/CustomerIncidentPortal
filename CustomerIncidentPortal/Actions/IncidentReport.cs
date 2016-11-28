using System;
using System.Collections.Generic;
using System.Linq;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    //Class Name: Incident Report
    //Author: Debbie, Zack, Grant
    //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for the incident report.
    //Methods in Class: Action()
    public class IncidentReport
    {
        //Method Name: Action
        //Purpose of the Method: This method calculates the average number of closed incidents per month for each employee in the database.
        //Arguments in Method: No arguments passed in this method. 
        public static void Action()
        {
            Console.Clear();
            Banner.Action();

            Console.WriteLine("\r\nEmployee                                   Incidents                   Average closed per month:\r\n");

            EmployeeFactory employeeFactory = EmployeeFactory.Instance;
            IncidentFactory incidentFactory = IncidentFactory.Instance;

            List<Employee> ListOfAllEmployees = employeeFactory.GetAllEmployees();
            List<Incident> ListOfAllIncidents = incidentFactory.GetAllIncidents();

            foreach(Employee employee in ListOfAllEmployees)
            {
                List<Incident> ListOfIncidents = ListOfAllIncidents.Where(i => i.EmployeeId == employee.EmployeeId).ToList();
                int numberOfOpenIncidents = ListOfIncidents.Where(i => i.IsResolved == "false").Count();
                int numberOfClosedIncidents = ListOfIncidents.Where(i => i.IsResolved == "True").Count();

                DateTime dateHired = employee.StartDate;

                Double averageClosedByMonth = numberOfClosedIncidents / Math.Floor(DateTime.Today.Subtract(dateHired).Days / (365.25 / 12));

                Console.WriteLine($"{employee.LastName, -10}, {employee.FirstName, -30} Open Incidents: {numberOfOpenIncidents, -10}  {averageClosedByMonth, -10}\r\n");


            }

            Console.WriteLine("Enter any key to return to the main menu.");
            Console.ReadLine();
            MainMenu.Action();
        }
    }
}
