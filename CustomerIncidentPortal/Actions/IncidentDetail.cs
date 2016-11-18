using System;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortal.Actions
{
    //Class Name: IncidentDetail
    //Authors: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class: The purpose of this class is to create the view that displays a single incident and the details of this incident.
    //Methods in Class:  Action().

    public class IncidentDetail
    {
        //Method Name: Action
        //Purpose of the Method: This method creates an instance of the incident, creates the view of the incident for the user, including all relevant information. It then takes input from the user to update the resolution of the incident as necessary, saves this on the active instance of the incident, and calls for the update method on the incident entity to save this to the database. It then takes input from the user to go back to the main menu.
        //Arguments in Method: This method does not take an argument. 

        public static void Action()
        {
            Console.Clear();
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            Incident ActiveIncident = incidentFactory.ActiveIncident;
            IncidentType ActiveIncidentType = incidentFactory.GetSingleIncidentType(ActiveIncident.IncidentTypeId);
            Console.WriteLine("****************INCIDENT DETAILS*****************");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Customer: {ActiveIncident.CustomerLastName}, {ActiveIncident.CustomerFirstName}\t\t\t Order: {ActiveIncident.OrderId}");
            Console.WriteLine($"Incident Type: {ActiveIncidentType.IncidentTypeName}");
            Console.WriteLine($"Labels: \n *{ActiveIncidentType.Label1}");
            if (ActiveIncidentType.Label2 != "")
            {
                Console.WriteLine($" *{ActiveIncidentType.Label2}");
            }
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
