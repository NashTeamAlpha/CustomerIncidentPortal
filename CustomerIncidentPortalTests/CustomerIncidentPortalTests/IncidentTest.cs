﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Factories;

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

            //TODO Customer Entity, Customer Factory
       
        }

                //CustomerFactory.GetCustomerOrders(Customers[0].CustomerId);
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
            //Assert.AreEqual(ShouldBeIncident.IncidentTypeId, TestIncident.IncidentTypeId);

            //Take in input from user name, etc
            //Goes to old DB
            //Gets all orders for that name

        }
     }
}
