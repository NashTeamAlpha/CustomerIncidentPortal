using System;
using System.Collections.Generic;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Factories
{
    public class EmployeeFactory
    {

        private static EmployeeFactory _instance;
        public static EmployeeFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmployeeFactory();
                }
                return _instance;
            }
        }

        private Employee _activeEmployee = null;
        public Employee ActiveEmployee
        {
            get
            {
                return _activeEmployee;
            }
            set
            {
                _activeEmployee = value;
            }
        }

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
                reader.Close();
            });

            return e;
        }

        public List<Employee> GetEmployeeByName (String FirstName, String LastName)
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            List<Employee> EmployeeList = new List<Employee>();

            conn.execute($"SELECT EmployeeId, FirstName, LastName, IsAdmin, DepartmentId, StartDate FROM Employees WHERE Employees.FirstName = '{FirstName}' AND Employees.LastName = '{LastName}'",
            (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    EmployeeList.Add(new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        FirstName = reader[1].ToString(),
                        LastName = reader[2].ToString(),
                        IsAdmin = reader[3].ToString(),
                        DepartmentId = reader.GetInt32(4),
                        StartDate = reader.GetDateTime(5)
                    });
                }
                reader.Close();
            });
            return EmployeeList;
        }

        public List<Employee> GetAllEmployees()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            List<Employee> ListOfAllEmployees = new List<Employee>();

            conn.execute($"SELECT EmployeeId, FirstName, LastName, IsAdmin, DepartmentId, StartDate FROM Employees",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        ListOfAllEmployees.Add(new Employee
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            IsAdmin = reader[3].ToString(),
                            DepartmentId = reader.GetInt32(4),
                            StartDate = reader.GetDateTime(5)
                        });
                    }
                    reader.Close();
                });
            return ListOfAllEmployees;
        }
    }
}
