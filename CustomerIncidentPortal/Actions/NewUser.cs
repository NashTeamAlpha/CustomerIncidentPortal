using System;
using System.Collections.Generic;
using System.Linq;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;


namespace CustomerIncidentPortal.Actions
{
    public class NewUser
    {
        //Class Name: NewUser
        //Author: Zack Repass, Grant Regnier, Debbie Bourne
        //Purpose of the class: The purpose of this class is to create the view that will prompt the user for the last name, first name, and department of a new user.
        //Methods in Class: Action().

        public static void Action()
        {
            //Method Name: Action
            //Purpose of the Method: This method creates the view so the last name and first name of a new employee can be entered and then the department can be chosen from a list. This information is then saved to the active instance of the employee, the save method on the Employee entity is called to save the new user in the database. Then the main menu view is called to return the user to the main menu.
            //Arguments in Method: This method does not take arguments 

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
            Employee savedEmployee = employeeFactory.getEmployee(employee);
            employee.EmployeeId = savedEmployee.EmployeeId;
            employeeFactory.ActiveEmployee = employee;
            MainMenu.Action();
            
        }
    
    }
}
