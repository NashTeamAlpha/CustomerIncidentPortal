using System;
using CustomerIncidentPortal.Factories;

namespace CustomerIncidentPortal.Actions
{
    //Class Name: MainMenu
    //Author: Zack Repass, Debbie Bourne, Grant Regnier
    //Purpose of the class: The purpose of this class is to display options in the console for what the employee can do and view. The main menu varies slightly for administrators and non-administrators.
    //Methods in Class: Action()
    public class MainMenu
    {
        //Method Name: Action
        //Purpose of the Method: This purpose of this method is to check to see whether the active employee is an administrator or not and display the appropriate options for that employee. All employees can create an incident and view his/her open and closed incidents. Administrators can also view a report of all employees, how many open incidents each employee has, and the average number of incidents that employee closes each month. This method will redirect the employee to the correct view based on his/her selection.
        //Arguments in Method: Need arguments are passed into this method. 
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
                IncidentReport.Action();
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
