using System;
using System.Collections.Generic;
using System.Linq;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    //Class Name: EmployeeFactory
    //Author: Grant Regnier, Zack Repass, Debbie Bourne
    //Purpose of the class: To list all incidents in the database with an EmployeeId matching the current ActiveEmployee singleton's EmployeeId. And to allow selection of the matching incidents to see detailed view.
    //Methods in Class: Action()
    public class ListIncidents
    {
        //Method Name: Action
        //Purpose of the Method: This method provides a list of Incidents for the user to select, then setting the ActiveIncident Singleton to the selected incident. This method then calls the IncidentDetail() method.
        //Arguments in Method: None.
        public static void Action()
        {
            Console.Clear();
            Banner.Action();

           Console.WriteLine("\r\nHere are your incidents: \r\n");

            EmployeeFactory employeeFactory = EmployeeFactory.Instance;
            IncidentFactory incidentFactory = IncidentFactory.Instance;

            List<Incident> ourIncidents = incidentFactory.GetIncidentsByEmployeeId(employeeFactory.ActiveEmployee.EmployeeId);

            foreach(Incident incident in ourIncidents)
            {
                Console.WriteLine($"{incident.IncidentId}. {incident.CustomerLastName}, {incident.CustomerFirstName} :  Order ID {incident.OrderId}");
            }

            try
            {
                int Choice = Int32.Parse(Console.ReadLine());

                if (Choice == 0)
                {
                    Action();
                }

                Incident SelectedIncident = ourIncidents.Where(i => i.IncidentId == Choice).Single();

                incidentFactory.ActiveIncident = SelectedIncident;
                IncidentDetail.Action();
            }

            catch
            {
                Action();
            }
            Console.ReadLine();
        }
    }
}
