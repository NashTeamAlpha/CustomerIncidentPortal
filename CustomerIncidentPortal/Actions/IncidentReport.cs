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
