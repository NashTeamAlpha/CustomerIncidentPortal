using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIncidentPortal.Factories;
using CustomerIncidentPortal.Entities;

namespace CustomerIncidentPortalTests
{
    //Class Name: DepartmentTest
    //Author: Grant Regnier, Debbie Bourne, Zack Repass
    //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to departments.
    //Methods in Class: DepartmentTest()

    [TestClass]
   public class DepartmentTest
    {

        //Method Name: TestDepartmentsCanBePulledFromDataBase
        //Purpose of the Method: This method tests whether or not a list of departments can be pulled from the database when creating a new user.
        //Arguments in Method: none

        [TestMethod]
        public void TestDepartmentsCanBePulledFromDataBase()
        {
           DepartmentFactory departmentFactory = new DepartmentFactory();
           List<Department> listOfDepartments = departmentFactory.GetAllDepartments();
           Assert.IsNotNull(listOfDepartments);
        }
    }
}
