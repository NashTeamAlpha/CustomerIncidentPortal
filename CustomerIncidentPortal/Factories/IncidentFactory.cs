using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace CustomerIncidentPortal.Factories
{
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
    }
}
