using System;
using CustomerIncidentPortal.Factories;

namespace CustomerIncidentPortal.Actions
{
    public class MainMenu
    {
        public static void Action()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;

            Console.Clear();
            Banner.Action();

            Console.WriteLine("\r\nWelcome To The Main Menu\r\n");
            Console.WriteLine("1. Create Incident");
            Console.WriteLine("2. List My Incidents");
            if (employeeFactory.ActiveEmployee.IsAdmin == "True")
            {
                Console.WriteLine("3. Administrative Report");
            }
            Console.WriteLine("x: Exit");
            var userInput = Console.ReadLine();
            if (userInput == "1")
            {
                CreateIncident.Action();
            }
           else if (userInput == "2")
            {
               ListIncidents.Action();
            }
            else if (userInput == "3" && employeeFactory.ActiveEmployee.IsAdmin == "True")
            {
                Console.WriteLine("IncidentReport.Action()");
            }
            else if (userInput == "x")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("That is not an option, press enter to try again.");
                Console.ReadLine();
                Action();
            }
        }
    }
}
