using System;
using System.Collections.Generic;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

namespace CustomerIncidentPortal.Factories
{
    //Class Name: EmployeeFactory
    //Author: Grant Regnier, Zack Repass, Debbie Bourne
    //Purpose of the class: The purpose of this class is to provide the application access to an employee factory singleton and a singleton of the employee. This also gets employees from the database with SQL querys passed to our connection files.
    //Methods in Class: getEmployee(Employee employee), GetEmployeeByName(string FirstName, string LastName), GetAllEmployees()
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

        //Method Name: GetEmployee(Employee employee)
        //Purpose of the Method: This method takes an instance of Employee, then retrieves that Employee from the Database.
        //Arguments in Method: An instance of Employee.
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

        //Method Name: GetEmployeeByName
        //Purpose of the Method: This method takes two string parameters and querys the Database with them where Employee first and last name are equal to them.
        //Arguments in Method: Takes two strings, the first is for the FirstName, the second is for the LastName.
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

        //Method Name: GetAllEmployees
        //Purpose of the Method: This Method Returns a List of all employees. 
        //Arguments in Method: No arguments
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
