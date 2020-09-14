using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CleanIT
{
    public class DatabaseHandler
    {
        private string connectionString = "Data Source=PC-BB-5987;Initial Catalog=cleanIt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private DataSet resultSet = new DataSet();
        public DataSet Execute(string query)
        {
            DataSet resultSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection(connectionString))))
            {
                adapter.Fill(resultSet);
            }
            return resultSet;
        }
    }
}
