using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseEmployeesCA
{
    class Database
    {
        private SqlConnection sqlConnection = null;

        private void OpenConnection()
        {
            try
            {
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBEmployees"].ConnectionString);
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при открытии соединения: {ex.Message}");
            }
        }

        private void CloseConnection()
        {
            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при закрытии соединения: {ex.Message}");
            }
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }

        public SqlDataReader ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            SqlDataReader reader = null;

            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    reader = command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }

            return reader;
        }

        public void BatchInsertData(string tableName, DataTable dataTable, int batchSize = 0)
        {
            try
            {
                OpenConnection();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    bulkCopy.DestinationTableName = tableName;

                    if (batchSize > 0)
                    {
                        bulkCopy.BatchSize = batchSize;
                    }

                    bulkCopy.WriteToServer(dataTable);
                }

                Console.WriteLine("Данные успешно добавлены");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении пакетной вставки данных: {ex.Message}");
            }
        }
    }
}
