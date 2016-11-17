using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    public class IncidentDetail
    {
        public static void Action()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            Incident ActiveIncident = incidentFactory.ActiveIncident;
            IncidentType ActiveIncidentType = incidentFactory.GetSingleIncidentType(ActiveIncident.IncidentTypeId);
            Console.WriteLine("****************INCIDENT DETAILS*****************");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Customer: {ActiveIncident.CustomerLastName}, {ActiveIncident.CustomerFirstName}\t\t\t Order: {ActiveIncident.OrderId}");
            Console.WriteLine($"Incident Type: {ActiveIncidentType.IncidentTypeName}");
            if (ActiveIncident.IsResolved == "false")
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Enter Resolution or Press X to Exit");
                string userInput = Console.ReadLine(); 
                if (userInput == "X" || userInput == "x")
                {
                    MainMenu.Action();
                }
                else if (userInput == "")
                {
                    Action();
                }
                else
                {
                    ActiveIncident.Resolution = userInput;
                    Incident ActiveIncidentFromDb = incidentFactory.getIncident(ActiveIncident);
                    ActiveIncident.IncidentId = ActiveIncidentFromDb.IncidentId;
                    ActiveIncident.Update();
                    MainMenu.Action();
                }
            }
            else
            {
                Console.WriteLine($"Resolution: \n {ActiveIncident.Resolution}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Press X to Exit");
                string userInput = Console.ReadLine();
                if (userInput == "X" || userInput == "x")
                {
                    MainMenu.Action();
                }
                else
                {
                    Action();
                }
            }
        }
    }
}
