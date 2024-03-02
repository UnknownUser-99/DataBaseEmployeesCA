using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataBaseEmployeesCA
{
    class Mode3
    {
        private List<Employee> employees = new List<Employee>();

        public void Launch()
        {
            Select();
            Read();
        }

        private void Select()
        {
            try
            {
                Database DB = new Database();

                string query = "SELECT DISTINCT fullName, birthDate, gender FROM Employees ORDER BY fullName";

                SqlDataReader reader = DB.ExecuteReader(query);

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
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при извлечении данных: {ex.Message}");
            }
        }

        private void Read()
        {
            Console.WriteLine("Справочник сотрудников:");

            foreach (var employee in employees)
            {
                int age = employee.CalculateAge();

                Console.WriteLine($"ФИО: {employee.FullName}, Дата рождения: {employee.BirthDate}, Возраст: {age} лет, Пол: {employee.Gender}");
            }

            employees.Clear();
        }
    }
}
