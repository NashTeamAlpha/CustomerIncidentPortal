using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;

namespace CustomerIncidentPortalTests
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void TestEmployeeCanBeCreated()
        {
            Employee Bob = new Employee();
            Assert.IsNotNull(Bob);
            Bob.EmployeeId = 1;
            Bob.FirstName = "Bob";
            Bob.LastName = "Sanders";
            Bob.IsAdmin = "false";
            Bob.DepartmentId = 2;
            Bob.StartDate = DateTime.Today;
            Assert.AreEqual(Bob.EmployeeId, 1);
            Assert.AreEqual(Bob.FirstName, "Bob");
            Assert.AreEqual(Bob.LastName, "Sanders");
            Assert.AreEqual(Bob.IsAdmin, "false");
            Assert.AreEqual(Bob.DepartmentId, 2);
            Assert.AreEqual(Bob.StartDate, DateTime.Today);
        }

       [TestMethod]
       public void TestEmployeeCanBeSavedToDB()
        {
            Employee Bob = new Employee();
            Bob.FirstName = "Bob";
            Bob.LastName = "Sanders";
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
        }

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

        [TestMethod]
        public void TestEmployeeFactoryIsASingleton()
        {
            EmployeeFactory employeeFactory = EmployeeFactory.Instance;
            EmployeeFactory employeeFactory2 = EmployeeFactory.Instance;
            Assert.AreEqual(employeeFactory, employeeFactory2);
        }

        [TestMethod]
        public void TestEmployeeCanBeSelectedByNameAndMultiplesCanBeReturned ()
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
        }
    }
}
