using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortalTests
{
    //Class Name: EmployeeTest
    //Author: Zack Repass, Debbie Bourne, Grant Regnier
    //Purpose of the class: The purpose of this class is to test the core functionality of an employee
    //Methods in Class: TestEmployeeCanBeCreated(), TestEmployeeCanBeSavedToDb(), TestEmployeeIsASingleton(), TestEmployeeFactoryIsASingleton(), TestEmployeeCanBeSelectedByNameAndMultipleCanBeReturned(), TestCanGetAllEmployees()
    [TestClass]
    public class EmployeeTest
    {
        //Method Name: TestEmployeeCanBeCreated
        //Purpose of the Method: To ensure that an object of type Employee can be created
        //Arguments in Method: No arguments passed to this method 
        [TestMethod]
        public void TestEmployeeCanBeCreated()
        {
            Employee Bob = new Employee();
            Assert.IsNotNull(Bob);
            Bob.EmployeeId = 1;
            Bob.FirstName = "EmployeeFirstNameTest";
            Bob.LastName = "EmployeeLastNameTest";
            Bob.IsAdmin = "false";
            Bob.DepartmentId = 2;
            Bob.StartDate = DateTime.Today;
            Assert.AreEqual(Bob.EmployeeId, 1);
            Assert.AreEqual(Bob.FirstName, "EmployeeFirstNameTest");
            Assert.AreEqual(Bob.LastName, "EmployeeLastNameTest");
            Assert.AreEqual(Bob.IsAdmin, "false");
            Assert.AreEqual(Bob.DepartmentId, 2);
            Assert.AreEqual(Bob.StartDate, DateTime.Today);
        }

        //Method Name: TestEmployeeCanBeSavedToDB
        //Purpose of the Method: To ensure that an object of type Employee can be created, saved to the database, and returned successfully 
        //Arguments in Method: No arguments passed to this method 
        [TestMethod]
        public void TestEmployeeCanBeSavedToDB()
        {
            Employee Bob = new Employee();
            Bob.FirstName = "EmployeeFirstNameTest";
            Bob.LastName = "EmployeeLastNameTest";
            Bob.IsAdmin = "false";
            Bob.DepartmentId = 2;
            Bob.StartDate = DateTime.Today;

            Bob.Save();
            EmployeeFactory employeeFactory = new EmployeeFactory();
            Employee shouldBeBob = employeeFactory.getEmployee(Bob);
            Assert.AreEqual(shouldBeBob.FirstName, Bob.FirstName);
            Assert.AreEqual(shouldBeBob.LastName, Bob.LastName);
            Assert.AreEqual(shouldBeBob.IsAdmin, Bob.IsAdmin);
            Assert.AreEqual(shouldBeBob.DepartmentId, Bob.DepartmentId);
            Assert.AreEqual(shouldBeBob.StartDate, Bob.StartDate);

            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert("DELETE FROM Employees WHERE Employees.FirstName = 'EmployeeFirstNameTest'");
        }

        //Method Name: TestEmployeeIsASingleton
        //Purpose of the Method: To ensure that there is only one active employee at a time 
        //Arguments in Method: No arguments passed to this method
        [TestMethod]
        public void TestEmployeeIsASingleton()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;

            Employee Bob = new Employee();
            Bob.FirstName = "Bob";
            Bob.LastName = "Sanders";
            Bob.IsAdmin = "false";
            Bob.DepartmentId = 2;
            Bob.StartDate = DateTime.Today;

            employeeFactory.ActiveEmployee = Bob;
            Assert.AreEqual(employeeFactory.ActiveEmployee, Bob);

            Employee Test = new Employee();
            Test.FirstName = "Test";
            Test.LastName = "Employee";
            Test.IsAdmin = "false";
            Test.DepartmentId = 2;
            Test.StartDate = DateTime.Today;

            employeeFactory.ActiveEmployee = Test;
            Assert.AreEqual(employeeFactory.ActiveEmployee, Test);
        }

        //Method Name: TestEmployeeFactoryIsASingleton
        //Purpose of the Method: To reinforce that there can only be one active employee at a time. Is essential to the singleton pattern because if more than one EmployeeFactory can be created then more than one employee singleton can be created. 
        //Arguments in Method: No arguments passed to this method
        [TestMethod]
        public void TestEmployeeFactoryIsASingleton()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;
            EmployeeFactory employeeFactory2 = EmployeeFactory.Instance;
            Assert.AreEqual(employeeFactory, employeeFactory2);
        }

        //Method Name: TestEmployeeCanBeSelectedByNameAndMultiplesCanBeReturned
        //Purpose of the Method: To test that the user can type in a first and last name for an employee and retrieve all instances of employees with that name from the database 
        //Arguments in Method: No arguments passed to this method
        [TestMethod]
        public void TestEmployeeCanBeSelectedByNameAndMultiplesCanBeReturned()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;

            Employee SelectByName = new Employee();
            SelectByName.FirstName = "FirstNameTest";
            SelectByName.LastName = "LastNameTest";
            SelectByName.IsAdmin = "false";
            SelectByName.DepartmentId = 2;
            SelectByName.StartDate = DateTime.Today;
            SelectByName.Save();

            Employee SelectByName2 = new Employee();
            SelectByName2.FirstName = "FirstNameTest";
            SelectByName2.LastName = "LastNameTest";
            SelectByName2.IsAdmin = "true";
            SelectByName2.DepartmentId = 1;
            SelectByName2.StartDate = DateTime.Today;
            SelectByName2.Save();

            List<Employee> ListOfEmployeesWithSameName = employeeFactory.GetEmployeeByName("FirstNameTest", "LastNameTest");

            Assert.AreEqual(ListOfEmployeesWithSameName[0].FirstName, SelectByName.FirstName);
            Assert.AreEqual(ListOfEmployeesWithSameName[0].LastName, SelectByName.LastName);
            Assert.AreEqual(ListOfEmployeesWithSameName[0].IsAdmin, SelectByName.IsAdmin);
            Assert.AreEqual(ListOfEmployeesWithSameName[0].DepartmentId, SelectByName.DepartmentId);
            Assert.AreEqual(ListOfEmployeesWithSameName[0].StartDate, SelectByName.StartDate);

            Assert.AreEqual(ListOfEmployeesWithSameName[1].FirstName, SelectByName2.FirstName);
            Assert.AreEqual(ListOfEmployeesWithSameName[1].LastName, SelectByName2.LastName);
            Assert.AreEqual(ListOfEmployeesWithSameName[1].IsAdmin, SelectByName2.IsAdmin);
            Assert.AreEqual(ListOfEmployeesWithSameName[1].DepartmentId, SelectByName2.DepartmentId);
            Assert.AreEqual(ListOfEmployeesWithSameName[1].StartDate, SelectByName2.StartDate);

            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert("DELETE FROM Employees WHERE Employees.FirstName = 'FirstNameTest'");
        }

        //Method Name: TestEmployeeGetAllEmployees
        //Purpose of the Method: To test that we can get all of the Employees to display on the incident report 
        //Arguments in Method: No arguments passed to this method
        [TestMethod]
        public void TestCanGetAllEmployees()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;

            List<Employee> ListOfAllEmployees = employeeFactory.GetAllEmployees();
            Assert.IsTrue(ListOfAllEmployees.Count > 0);
        }
    }
}
