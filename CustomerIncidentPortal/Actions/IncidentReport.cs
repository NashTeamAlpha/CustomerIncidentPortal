using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    public class IncidentReport
    {
        public static void Action()
        {
            Console.Clear();
            Banner.Action();

            Console.WriteLine("\r\nHere are all of the employees, incidents and average closed per month:\r\n");

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

                Double averageClosedByMonth = Math.Floor(DateTime.Today.Subtract(dateHired).Days / (365.25 / 12))/numberOfClosedIncidents;

                Console.WriteLine($"{employee.LastName}, {employee.FirstName} Open Incidents: {numberOfOpenIncidents}  {averageClosedByMonth}\r\n");


            }

            Console.WriteLine("Enter any key to return to the main menu.");
            Console.ReadLine();
            MainMenu.Action();
        }
    }
}
