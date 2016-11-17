using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortal.Entities
{
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

        public void Save()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert($"insert into Incidents (Resolution, IsResolved, EmployeeId, OrderId, IncidentTypeId, CustomerFirstName, CustomerLastName) values ('{this.Resolution}','{this.IsResolved}', '{this.EmployeeId}', '{this.OrderId}', '{this.IncidentTypeId}', '{this.CustomerFirstName}', '{this.CustomerLastName}')");
        }
        public void Update()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert($"update Incidents set Resolution = '{this.Resolution}', IsResolved = 'True' where IncidentId = '{this.IncidentId}'"); 
        }
    }
}

