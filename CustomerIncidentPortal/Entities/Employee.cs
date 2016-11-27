using System;
using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortal.Entities
{
    //Class Name: Employee
    //Author: Zack Repass, Grant Regnier, Debbie Bourne
    //Purpose of the class: The purpose of this class is to create a model of the properties and methods on an instance of an employee.
    //Methods in Class: Save().

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsAdmin { get; set; }
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }

        //Method Name: Save
        //Purpose of the Method: This method creates the appropriate database query then saves the new user to the database
        //Arguments in Method: This method does not take arguments 

        public void Save()
        {
            string query = $"insert into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) values ('{ this.FirstName}', '{ this.LastName}', '{ this.IsAdmin}', '{ this.DepartmentId}', '{ this.StartDate.ToString()}'); ";
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert(query);
        }
    }


}
