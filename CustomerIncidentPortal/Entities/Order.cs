namespace CustomerIncidentPortal.Entities
{
    //Class Name: Order
    //Author:Jamie, Chris, Delaine
    //Purpose of the class: The purpose of this class is to act as a model for our SQL reader to format data from a temporary table returned from a SQL query to the Database. 
    //Methods in Class: None.
    public class Order
    {
        public int OrderId { get; set; }
        public string DateCompleted { get; set; }
        public int CustomerId { get; set; }
    }
}
