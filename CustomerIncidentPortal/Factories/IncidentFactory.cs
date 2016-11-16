using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerIncidentPortal.Entities;
using CustomerIncidentPortal.Data;
using Microsoft.Data.Sqlite;

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
            });
            return i;
        }
    }
}
