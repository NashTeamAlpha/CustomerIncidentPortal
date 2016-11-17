using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortalTests
{
    [TestClass]
    public class IncidentTest
    {
        [TestMethod]
        public void TestGetCustomerToAddToIncident()
        {
            Customer TestCustomer = new Customer();
            TestCustomer.FirstName = "James";
            TestCustomer.LastName = "Regnier";
            CustomerFactory customerFactory = new CustomerFactory();
            List<Customer> Customers = customerFactory.GetCustomer(TestCustomer);
            for (var i =0; i < Customers.Count; i++)
            {
                Assert.AreEqual(TestCustomer.FirstName, Customers[i].FirstName);
                Assert.AreEqual(TestCustomer.LastName, Customers[i].LastName);
            }
        }

        [TestMethod]
        public void TestGetOrdersToAddToIncidentThatMatchCustomer()
        {
            Order testOrder = new Order();
            testOrder.CustomerId = 1;
            CustomerFactory customerFactory = new CustomerFactory();
            List<Order> orders = customerFactory.GetOrders(testOrder.CustomerId);
            for (var i = 0; i < orders.Count; i++)
            {
                Assert.AreEqual(testOrder.CustomerId, orders[i].CustomerId);
            }
        }

        [TestMethod]
        public void TestIncidentIsASingleton()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;

            Incident TestIncident = new Incident();
            TestIncident.IncidentId = 1;
            TestIncident.Resolution = "Resolved";
            TestIncident.IsResolved = "True";
            TestIncident.EmployeeId = 1;
            TestIncident.OrderId = 1;
            TestIncident.CustomerFirstName = "James";
            TestIncident.CustomerLastName = "Regnier";
            TestIncident.IncidentTypeId = 1;

            incidentFactory.ActiveIncident = TestIncident;
            Assert.AreEqual(incidentFactory.ActiveIncident, TestIncident);

            Incident TestIncident2 = new Incident();
            TestIncident2.IncidentId = 2;
            TestIncident2.Resolution = "";
            TestIncident2.IsResolved = "false";
            TestIncident2.EmployeeId = 2;
            TestIncident2.OrderId = 45;
            TestIncident2.CustomerFirstName = "TestCustomerFirstName";
            TestIncident2.CustomerLastName = "TestCustomerLastName";
            TestIncident2.IncidentTypeId = 1;

            incidentFactory.ActiveIncident = TestIncident2;
            Assert.AreEqual(incidentFactory.ActiveIncident, TestIncident2);
        }

        [TestMethod]
        public void TestIncidentFactoryIsASingleton()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            IncidentFactory incidentFactory2 = IncidentFactory.Instance;
            Assert.AreEqual(incidentFactory, incidentFactory2);
        }

        [TestMethod]
        public void TestCanCreateIncident()
        {
            Incident TestIncident = new Incident();
            Assert.IsNotNull(TestIncident);
            TestIncident.IncidentId = 1;
            TestIncident.Resolution = "Resolved";
            TestIncident.IsResolved = "True";
            TestIncident.EmployeeId = 1;
            TestIncident.OrderId = 1;
            TestIncident.CustomerFirstName = "James";
            TestIncident.CustomerLastName = "Regnier";
            TestIncident.IncidentTypeId = 1;
            Assert.AreEqual(TestIncident.IncidentId, 1);
            Assert.AreEqual(TestIncident.Resolution, "Resolved");
            Assert.AreEqual(TestIncident.IsResolved, "True");
            Assert.AreEqual(TestIncident.EmployeeId, 1);
            Assert.AreEqual(TestIncident.OrderId, 1);
            Assert.AreEqual(TestIncident.CustomerFirstName, "James");
            Assert.AreEqual(TestIncident.CustomerLastName, "Regnier");
            Assert.AreEqual(TestIncident.IncidentTypeId, 1);
        }

        [TestMethod]
        public void TestCanSaveIncidentToDatabase()
        {
            Incident TestIncident = new Incident();
            TestIncident.Resolution = "Resolved";
            TestIncident.IsResolved = "True";
            TestIncident.EmployeeId = 1;
            TestIncident.OrderId = 1;
            TestIncident.CustomerFirstName = "James";
            TestIncident.CustomerLastName = "Regnier";
            TestIncident.IncidentTypeId = 1;
            TestIncident.Save();
            IncidentFactory incidentFactory = new IncidentFactory();
            Incident ShouldBeIncident = incidentFactory.getIncident(TestIncident);
            Assert.AreEqual(ShouldBeIncident.Resolution, TestIncident.Resolution);
            Assert.AreEqual(ShouldBeIncident.IsResolved, TestIncident.IsResolved);
            Assert.AreEqual(ShouldBeIncident.EmployeeId, TestIncident.EmployeeId);
            Assert.AreEqual(ShouldBeIncident.OrderId, TestIncident.OrderId);
            Assert.AreEqual(ShouldBeIncident.CustomerFirstName, TestIncident.CustomerFirstName);
            Assert.AreEqual(ShouldBeIncident.CustomerLastName, TestIncident.CustomerLastName);
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert("DELETE FROM Incidents WHERE Incidents.Resolution = 'Resolved' and Incidents.EmployeeId = '1'");

        }

        [TestMethod]
        public void TestCanCreateOrder()
        {
            Order TestOrder = new Order();
            Assert.IsNotNull(TestOrder);
            TestOrder.OrderId = 1;
            TestOrder.DateCompleted = "2016-11-10";
            TestOrder.CustomerId = 1;
            Assert.AreEqual(TestOrder.OrderId, 1);
            Assert.AreEqual(TestOrder.DateCompleted, "2016-11-10");
            Assert.AreEqual(TestOrder.CustomerId, 1);
        }

        [TestMethod]
        public void TestCanCreateIncidentType()
        {
            IncidentType TestIncidentType = new IncidentType();
            Assert.IsNotNull(TestIncidentType);
            TestIncidentType.IncidentTypeId = 1;
            TestIncidentType.IncidentTypeName = "Defective Product";
            Assert.AreEqual(TestIncidentType.IncidentTypeId, 1);
            Assert.AreEqual(TestIncidentType.IncidentTypeName, "Defective Product");
        }

        [TestMethod]
        public void TestCanGetIncident()
        {
            IncidentFactory incidentFactory = new IncidentFactory();
            List<IncidentType> ListOfIncidentTypes = incidentFactory.GetIncidentTypes();
            Assert.AreEqual(ListOfIncidentTypes.Count, 7);
        }

        [TestMethod]
        public void TestCanUpdateIncidentInDatabase()
        {
            Incident TestIncident = new Incident();
            TestIncident.IncidentId = 1;
            TestIncident.Resolution = "";
            TestIncident.IsResolved = "false";
            TestIncident.EmployeeId = 1;
            TestIncident.OrderId = 1;
            TestIncident.CustomerFirstName = "James";
            TestIncident.CustomerLastName = "Regnier";
            TestIncident.IncidentTypeId = 1;
            TestIncident.Save();
            TestIncident.Resolution = "Resolved";
            TestIncident.IsResolved = "True";
            TestIncident.Update();
            IncidentFactory incidentFactory = new IncidentFactory();
            incidentFactory.getIncident(TestIncident);
            Assert.AreEqual(TestIncident.IsResolved, "True");
            Assert.AreEqual(TestIncident.Resolution, "Resolved");
            Assert.AreEqual(TestIncident.IncidentTypeId, 1);
            Assert.AreEqual(TestIncident.CustomerFirstName, "James");
            Assert.AreEqual(TestIncident.CustomerLastName, "Regnier");
            Assert.AreEqual(TestIncident.EmployeeId, 1);
            Assert.AreEqual(TestIncident.IncidentId, 1);
            Assert.AreEqual(TestIncident.OrderId, 1);
        }
        [TestMethod]
        public void TestGetSingleIncidentTypeNameFromDatabase()
        {
            IncidentFactory incidentFactory = new IncidentFactory();
            IncidentType IncidentTypeFromDb = incidentFactory.GetSingleIncidentType(1);
            Assert.AreEqual(IncidentTypeFromDb.IncidentTypeId, 1);
            Assert.AreEqual(IncidentTypeFromDb.IncidentTypeName, "Defective Product");
        }

    }
}
