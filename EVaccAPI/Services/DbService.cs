using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class DbService :IDisposable
    {
        SqlConnection connection;
        static DbService dbService;
        private DbService()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EVaccAPIContext"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            connection.Dispose();
            connection.Close();
        }

        public static DbService GetDbService()
        {
            if(dbService == null)
            {
                dbService = new DbService();
            }
            return dbService;
        }

        public void ExecuteNonQuery(string query)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (var command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public DataTable ExecuteReader(string query)
        {
            var data = new DataTable();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (var command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                adapter.Dispose();
            }                    
            return data;
        }

        public object ExecuteScalar(string query)
        {
            object data = null;
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (var command = new SqlCommand(query, connection))
            {
                data = command.ExecuteScalar();               
            }
            return data;
        }
    }
}