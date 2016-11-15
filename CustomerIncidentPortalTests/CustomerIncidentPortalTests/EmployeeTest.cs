using System;
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
    }
}
