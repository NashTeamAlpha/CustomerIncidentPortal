using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace CustomerIncidentPortal.Factories
{
    //Class Name: IncidentFactory
    //Author: Delaine Wendling, Chris Smalley, Jamie Duke
    //Purpose of the class: Creating a singleton for an active incident, retrieving an incident from the database, retrieving a list of incidents from the database by querying the employee id, retrieving incident types from the database, and anything else handling the Incident entity. 
    //Methods in Class: getInident(), GetIncidentsByEmployeeId(), GetIncidentTypes(), GetAllIncidents(),  GetSingleIncidentType()
    public class IncidentFactory
    {
        private static IncidentFactory _instance;
        public static IncidentFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IncidentFactory();
                }
                return _instance;
            }
        }

        private Incident _activeIncident = null;
        public Incident ActiveIncident
        {
            get
            {
                return _activeIncident;
            }
            set
            {
                _activeIncident = value;
            }
        }
        //Method Name: getIncident
        //Purpose of the Method: this method takes an instance of Incident and takes that incident from the db
        //Arguments in Method:  an instance of Incident
        public Incident getIncident(Incident incident)
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            Incident i = null;
            conn.execute($"select IncidentId, CustomerFirstName, CustomerLastName, i.OrderId, it.IncidentTypeName, Resolution, EmployeeId, IsResolved from Incidents i join IncidentTypes it on i.IncidentTypeId = it.IncidentTypeId where CustomerFirstName = '{incident.CustomerFirstName}' and CustomerLastName = '{incident.CustomerLastName}' and OrderId = '{incident.OrderId}' and i.IncidentTypeId = {incident.IncidentTypeId}; ", (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    i = new Incident{ 
                        IncidentId = reader.GetInt32(0),
                        CustomerFirstName = reader[1].ToString(),
                        CustomerLastName = reader[2].ToString(),
                        OrderId = reader.GetInt32(3),
                        IncidentTypeName = reader[4].ToString(),
                        Resolution = reader[5].ToString(),
                        EmployeeId = reader.GetInt32(6),
                        IsResolved = reader[7].ToString()
                    };
                }
                reader.Close();
            });
            return i;
        }

        //Method Name:  GetIncidentsByEmployeeId
        //Purpose of the Method: this method creates a list of incidents from a specific employeeId
        //Arguments in Method:  an EmployeeId int
        public List<Incident> GetIncidentsByEmployeeId(int EmployeeId)
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            List<Incident> listOfIncidents = new List<Incident>();

            conn.execute($"SELECT Incidents.IncidentId, Incidents.Resolution, Incidents.IsResolved, Incidents.EmployeeId, Incidents.OrderId, Incidents.CustomerFirstName, Incidents.CustomerLastName, Incidents.IncidentTypeId FROM Incidents Where Incidents.EmployeeId = {EmployeeId}",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        listOfIncidents.Add(new Incident
                        {
                            IncidentId = reader.GetInt32(0),
                            Resolution = reader[1].ToString(),
                            IsResolved = reader[2].ToString(),
                            EmployeeId = reader.GetInt32(3),
                            OrderId = reader.GetInt32(4),
                            CustomerFirstName = reader[5].ToString(),
                            CustomerLastName = reader[6].ToString(),
                            IncidentTypeId = reader.GetInt32(7)
                        });
                    }
                    reader.Close();
                });
            return listOfIncidents;
        }

        //Method Name:  GetIncidentTypes
        //Purpose of the Method: this method creates a list of incidents from a specific incidentType
        //Arguments in Method: n/a
        public List<IncidentType> GetIncidentTypes()
        {
            CustomerIncidentConnection Conn = new CustomerIncidentConnection();
            List<IncidentType> incidentTypeList = new List<IncidentType>();

            string query = $"select IncidentTypeId, IncidentTypeName from IncidentTypes";
            Conn.execute(query, (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    incidentTypeList.Add(new IncidentType
                    {
                        IncidentTypeId = reader.GetInt32(0),
                        IncidentTypeName = reader[1].ToString(),
                    });
                }
                reader.Close();
            });
            return incidentTypeList;
        }


        //Method Name:  GetAllIncients
        //Purpose of the Method: this method creates a list of every incident in the db
        //Arguments in Method: n/a
        public List<Incident> GetAllIncidents()
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            List<Incident> ListOfAllIncidents = new List<Incident>();

            conn.execute($"SELECT IncidentId, Resolution, IsResolved, EmployeeId, OrderId, CustomerFirstName, CustomerLastName, IncidentTypeId FROM Incidents",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        ListOfAllIncidents.Add(new Incident
                        {
                            IncidentId = reader.GetInt32(0),
                            Resolution = reader[1].ToString(),
                            IsResolved = reader[2].ToString(),
                            EmployeeId = reader.GetInt32(3),
                            OrderId = reader.GetInt32(4),
                            CustomerFirstName = reader[5].ToString(),
                            CustomerLastName = reader[6].ToString(),
                            IncidentTypeId = reader.GetInt32(7)
                        });
                    }
                    reader.Close();
                });
            return ListOfAllIncidents;
        }

        //Method Name:  GetSingleIncidentType
        //Purpose of the Method: this method retrieves the IncidentType from the database to list on the incident detail action
        //Arguments in Method: a specific incidentType int
        public IncidentType GetSingleIncidentType(int IncidentTypeId)
        {
            CustomerIncidentConnection conn = new CustomerIncidentConnection();
            IncidentType it = null;
            conn.execute($"select IncidentTypeId, IncidentTypeName, Label1, Label2 from IncidentTypes where IncidentTypeId = '{IncidentTypeId}'; ", (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    it = new IncidentType
                    {
                        IncidentTypeId = reader.GetInt32(0),
                        IncidentTypeName = reader[1].ToString(),
                        Label1 = reader[2].ToString(),
                        Label2 = reader[3].ToString()
                    };
                }
                reader.Close();
            });
            return it;
        }
    }
}
