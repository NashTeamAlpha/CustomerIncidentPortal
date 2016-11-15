using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Factories
{
    public class EmployeeFactory
    {
        public Employee getEmployee(Employee employee)
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            Employee e = null;

            conn.execute($"select EmployeeId, FirstName, LastName, IsAdmin, DepartmentId, StartDate From Employees Where Employees.FirstName = '{employee.FirstName}' And Employees.LastName = '{employee.LastName}' And Employees.DepartmentId = '{employee.DepartmentId}'",
            (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    e = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        FirstName = reader[1].ToString(),
                        LastName = reader[2].ToString(),
                        IsAdmin = reader[3].ToString(),
                        DepartmentId = reader.GetInt32(4),
                        StartDate = reader.GetDateTime(5)
                    };
                }
            });
            return e;
        }
    }
}
