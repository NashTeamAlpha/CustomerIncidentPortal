using CustomerIncidentPortal.Actions;

namespace CustomerIncidentPortal
{
    //Class Name: Program
    //Author: Zack Repass, Grant Regnier, Debbie Bourne, Chris Smalley, Jamie Duke, Delaine Wendling
    //Purpose of the class: The purpose of this class is to manage the Main method that tells the program where to start.
    //Methods in Class: Main().

    class Program
    {
        //Method Name: Main
        //Purpose of the Method: This method is the entry point of the program. It calls the method that will display the first view for the user.
        //Arguments in Method: This method can take arguments but in this case it does not. 

        static void Main(string[] args)
        {
            ChooseExistingEmployee.Action();
        }
    }
}
