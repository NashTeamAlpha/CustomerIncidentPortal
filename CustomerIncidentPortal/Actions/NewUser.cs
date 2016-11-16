using System;
using System.Collections.Generic;
using System.Linq;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;


namespace CustomerIncidentPortal.Actions
{
    public class NewUser
    {
        public static void Action()
        {
            Console.Clear();
            Banner.Action();

            DepartmentFactory departmentFactory = new DepartmentFactory();

            EmployeeFactory employeeFactory = EmployeeFactory.Instance;

            List<Department> allDepartments = departmentFactory.GetAllDepartments();

            Employee employee = new Employee();

            Console.WriteLine("\r\nEnter Employee First Name");
            employee.FirstName = Console.ReadLine();
            if (employee.FirstName == "")
            {
                Action();
            }

            Console.WriteLine("\r\nEnter Employee Last Name");
            employee.LastName = Console.ReadLine();
            if (employee.LastName == "")
            {
                Action();
            }

            Console.WriteLine("\r\nChoose Your Employee's Department");

            foreach (Department department in allDepartments)
            {
                Console.WriteLine($"DepartmentId:{department.DepartmentId} {department.DepartmentName}");
            }

            try
            {
                int Choice = Int32.Parse(Console.ReadLine());

                if (Choice == 0)
                {
                    Action();
                }

                Department SelectedDepartment = allDepartments.Where(d => d.DepartmentId == Choice).Single();

                employee.DepartmentId = SelectedDepartment.DepartmentId;

            }

            catch
            {
                Console.WriteLine("\r\nThat was not one of the options\r\n");
                Action();
            }

            employee.IsAdmin = "false";

            employee.StartDate = DateTime.Today;
            try
            {
                employee.Save();
            }
            catch
            {
                Action();
            }

            employeeFactory.ActiveEmployee = employee;
            MainMenu.Action();
            
        }
    
    }
}
