using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortal.Entities
{
    //Class Name: Incident
    //Author: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class : To format, save and update data for the SQL Incident table
    //Methods in Class: Save(), Update()
    public class Incident
    {
        public int IncidentId { get; set; }
        public string Resolution { get; set; }
        public string IsResolved { get; set; }
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string IncidentTypeName { get; set; }
        public int IncidentTypeId { get; set; }

        //Method Name: Save
        //Purpose of the Method: to open a connetion and save statements to the Incident table on the SQL db
        //Arguments in Method: n/a
        public void Save()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert($"insert into Incidents (Resolution, IsResolved, EmployeeId, OrderId, IncidentTypeId, CustomerFirstName, CustomerLastName) values ('{this.Resolution}','{this.IsResolved}', '{this.EmployeeId}', '{this.OrderId}', '{this.IncidentTypeId}', '{this.CustomerFirstName}', '{this.CustomerLastName}')");
        }

        //Method Name: Update
        //Purpose of the Method: to open a connetion and update the 'isResolved' collum to 'True' on the Incident table, indicating an incident is closed.
        //Arguments in Method: n/a
        public void Update()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert($"update Incidents set Resolution = '{this.Resolution}', IsResolved = 'True' where IncidentId = '{this.IncidentId}'"); 
        }
    }
}

