using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortalTests
{
    [TestClass]
   public class DepartmentTest
    {
        [TestMethod]
        public void TestDepartmentsCanBePulledFromDataBase()
        {
           DepartmentFactory departmentFactory = new DepartmentFactory();
           List<Department> listOfDepartments = departmentFactory.GetAllDepartments();
           Assert.IsNotNull(listOfDepartments);
        }
    }
}
