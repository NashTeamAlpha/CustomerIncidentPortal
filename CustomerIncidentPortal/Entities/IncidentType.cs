namespace CustomerIncidentPortal.Entities
{
    //Class Name: IncidentType
    //Author: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class: Creates variables to be inherited by all related incidents.
    //Methods in Class: none

    public class IncidentType
    {
        public int IncidentTypeId { get; set; }
        public string IncidentTypeName { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
    }
}
