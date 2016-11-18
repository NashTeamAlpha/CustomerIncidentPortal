using System;
namespace CustomerIncidentPortal.Actions
{
    //Class Name: Banner
    //Author: Debbie Bourne, Grant Regnier, Zack Repass
    //Purpose of the class: The purpose of this class is to manage the methods that will produce banner that appears at the top of the Main Menu view.
    //Methods in Class: Action()

    class Banner
    {

        //Method Name: Action
        //Purpose of the Method: This method loads a series of writeline commands to create the banner that appears at the top of the Main Menu view.
        //Arguments in Method: none

        public static void Action ()
        {
            string stars = "************************************************************";
            Console.WriteLine(stars);
            Console.WriteLine(stars);
            Console.WriteLine("          Bangazon Customer Incident Portal");
            Console.WriteLine(stars);
            Console.WriteLine(stars);
        }
    }
}
