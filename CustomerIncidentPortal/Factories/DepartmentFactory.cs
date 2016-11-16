using System.Collections.Generic;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Factories
{
    public class DepartmentFactory
    {
       public List<Department> GetAllDepartments()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            List<Department> DepartmentList = new List<Department>();
            conn.execute("SELECT DepartmentId, DepartmentName FROM Departments",
            (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    DepartmentList.Add(new Department
                    {
                        DepartmentId = reader.GetInt32(0),
                        DepartmentName = reader[1].ToString(),
                    });
                }
                reader.Close();
            });
            return DepartmentList;
        }
        
    }
}
