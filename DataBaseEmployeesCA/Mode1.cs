using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataBaseEmployeesCA
{
    class Mode1
    {
        private string TableName;

        public void Launch()
        {
            Console.WriteLine("Введите название таблицы:");
            TableName = Console.ReadLine();

            if (IsEnglishTableName())
            {
                Create();
            }
            else
            {
                Console.WriteLine("Введено некорректное название таблицы");
            }
        }

        private bool IsEnglishTableName()
        {
            string pattern = "^[a-zA-Z]+$";

            return Regex.IsMatch(TableName, pattern);
        }

        private void Create()
        {
            Database DB = new Database();

            try
            {
                string createTableQuery = $"CREATE TABLE {TableName} (id INT IDENTITY(1,1) PRIMARY KEY, fullName NVARCHAR(100), birthDate NVARCHAR(10), gender NVARCHAR(10))";
                DB.ExecuteNonQuery(createTableQuery);

                Console.WriteLine($"Таблица {TableName} успешно создана");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при создании таблицы: {ex.Message}");
            }
        }
    }
}
