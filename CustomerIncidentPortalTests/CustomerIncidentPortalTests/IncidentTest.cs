using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Data;

namespace CustomerIncidentPortalTests
{
    //Class Name: IncidentTest
    //Author: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class: This class is for testing that incidents can be created, customers can be assigned to customers, singletons for incident factories and incidents can be created, and that all of the above can be accessed and saved to the database. 
    //Methods in Class: TestGetCustomerToAddToIncident(), TestGetOrdersToAddToIncidentThatMatchCustomer(), TestIncidentIsASingleton(), TestIncidentFactoryIsASingleton(), TestCanCreateIncident(), TestCanSaveIncidentToDatabase(), TestCanCreateOrder(), TestCanCreateIncidentType(), TestCanGetIncident(), TestIncidentsCanBeSelectedByEmployeeId(), TestCanUpdateIncidentInDatabase(), TestCanGetAllIncidents(), TestGetSingleIncidentTypeNameFromDatabase()

    [TestClass]
    public class IncidentTest
    {
        //Method Name: TestGetCustomerToAddToIncident
        //Purpose of the Method: Testing that customer can be retrieved from a list of customers by first and last name. 
        //Arguments in Method: N/A
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
        //Method Name: TestGetOrdersToAddToIncidentThatMatchCustomer
        //Purpose of the Method: Testing that the correct order is grabbed from the selected customer.
        //Arguments in Method: N/A
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
        //Method Name: TestIncidentIsASingleton
        //Purpose of the Method: To ensure that there is only one active incident for each user at one time. 
        //Arguments in Method: N/A
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
        //Method Name: TestIncidentFactoryIsASingleton
        //Purpose of the Method: To reinforce that there can only be one active incident at a time. Is essential to the singleton pattern because if more than one IncidentFactory can be created then more than one incident singleton can be created.
        //Arguments in Method: N/A
        [TestMethod]
        public void TestIncidentFactoryIsASingleton()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;
            IncidentFactory incidentFactory2 = IncidentFactory.Instance;
            Assert.AreEqual(incidentFactory, incidentFactory2);
        }
        //Method Name: TestCanCreateIncident
        //Purpose of the Method: To ensure that an object of type Incident can be created
        //Arguments in Method: N/A
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
        //Method Name: TestCanSaveIncidentToDatabase
        //Purpose of the Method: To ensure that an object of type Incident can be saved to the db and returned successfully
        //Arguments in Method: N/A
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
        //Method Name: TestCanCreateOrder
        //Purpose of the Method: To ensure that an object of type Order can be created
        //Arguments in Method: N/A
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
        //Method Name: TestCanCreateIncidentType
        //Purpose of the Method: To ensure that an object of type IncidentType can be created
        //Arguments in Method: N/A
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
        //Method Name: TestCanGetIncident
        //Purpose of the Method: To ensure that an object of type IncidentType can be collected into a list
        //Arguments in Method: N/A
        [TestMethod]
        public void TestCanGetIncident()
        {
            IncidentFactory incidentFactory = new IncidentFactory();
            List<IncidentType> ListOfIncidentTypes = incidentFactory.GetIncidentTypes();
            Assert.AreEqual(ListOfIncidentTypes.Count, 7);
        }
        //Method Name: TestCanGetIncident
        //Purpose of the Method: To ensure that an object of type Incident can be retrieved with the EmployeeId
        //Arguments in Method: N/A
        [TestMethod]
        public void TestIncidentsCanBeSelectedByEmployeeId()
        {
            IncidentFactory incidentFactory = new IncidentFactory();

            Incident TestIncident = new Incident();
            TestIncident.Resolution = "Resolved";
            TestIncident.IsResolved = "True";
            List<Incident> IncidentsByEmployeeId = incidentFactory.GetIncidentsByEmployeeId(1);
            Assert.IsNotNull(IncidentsByEmployeeId);
        }
        //Method Name: TestCanUpdateIncidentInDatabase
        //Purpose of the Method: To ensure that an object of type Incident can be updated in the database
        //Arguments in Method: N/A
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
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            conn.insert("DELETE FROM Incidents WHERE Incidents.Resolution = 'Resolved' and Incidents.EmployeeId = '1' and Incidents.OrderId = '1'");
        }
        //Method Name: TestCanUpdateIncidentInDatabase
        //Purpose of the Method: To ensure that an object of type Incident can be updated in the database
        //Arguments in Method: N/A
        [TestMethod]
        public void TestCanGetAllIncidents()
        {
            IncidentFactory incidentFactory = IncidentFactory.Instance;

            List<Incident> ListOfAllIncidents = incidentFactory.GetAllIncidents();
            Assert.IsTrue(ListOfAllIncidents.Count > 0);
        }
        //Method Name: TestGetSingleIncidentTypeNameFromDatabase
        //Purpose of the Method: To ensure that an object of type IncidentType can be retrieved in the database
        //Arguments in Method: N/A
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
