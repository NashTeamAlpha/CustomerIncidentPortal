using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortal.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsAdmin { get; set; }
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }

        public void Save()
        {
            string query = $"insert into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) values ('{ this.FirstName}', '{ this.LastName}', '{ this.IsAdmin}', '{ this.DepartmentId}', '{ this.StartDate.ToString()}'); ";
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert(query);
        }
    }


}
