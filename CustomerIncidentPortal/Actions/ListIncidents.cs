using System;
using System.Collections.Generic;
using System.Linq;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    public class ListIncidents
    {
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
                IncidentDetails.Action();
            }

            catch
            {
                Action();
            }
            Console.ReadLine();
        }
    }
}
