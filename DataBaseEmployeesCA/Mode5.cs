using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEmployeesCA
{
    class Mode5
    {
        private List<Employee> employees = new List<Employee>();

        public void Launch()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long TimeQuery = Select();
            Read();

            stopwatch.Stop();

            Console.WriteLine($"Время выполнения запроса: {TimeQuery} мс");
            Console.WriteLine($"Общее время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }

        private long Select()
        {
            try
            {
                Database DB = new Database();

                string query = "SELECT fullName, birthDate, gender FROM Employees WHERE gender = 'male' AND fullName LIKE 'F%' ORDER BY fullName";

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                SqlDataReader reader = DB.ExecuteReader(query);

                stopwatch.Stop();

                long TimeQuery = stopwatch.ElapsedMilliseconds;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string fullName = reader["FullName"].ToString();
                        string birthDate = reader["BirthDate"].ToString();
                        string gender = reader["Gender"].ToString();

                        Employee employee = new Employee(fullName, birthDate, gender);
                        employees.Add(employee);
                    }
                }

                reader.Close();

                return TimeQuery;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при извлечении данных: {ex.Message}");

                long TimeQuery = 0;

                return TimeQuery;
            }
        }

        private void Read()
        {
            Console.WriteLine("Результат выборки:");

            foreach (var employee in employees)
            {
                int age = employee.CalculateAge();

                Console.WriteLine($"ФИО: {employee.FullName}, Дата рождения: {employee.BirthDate}, Возраст: {age} лет, Пол: {employee.Gender}");
            }

            Console.WriteLine($"Всего записей: {employees.Count}");

            employees.Clear();
        }
    }
}
