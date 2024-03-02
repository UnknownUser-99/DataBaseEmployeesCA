using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;

namespace DataBaseEmployeesCA
{
    class Employee
    {
        public readonly string FullName;
        public readonly string BirthDate;
        public readonly string Gender;

        public Employee(string fullName, string birthDate, string gender)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
        }

        public int CalculateAge()
        {
            DateTime currentDate = DateTime.Now;

            DateTime birthDateTime = DateTime.ParseExact(BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            int age = currentDate.Year - birthDateTime.Year;

            if (currentDate < birthDateTime.AddYears(age))
            {
                age--;
            }

            return age;
        }

        public void AddToDatabase()
        {
            Database DB = new Database();

            try
            {
                string query = "INSERT INTO Employees (fullName, birthDate, gender) VALUES (@FullName, @BirthDate, @Gender)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@FullName", FullName),
                new SqlParameter("@BirthDate", BirthDate),
                new SqlParameter("@Gender", Gender)
                };

                DB.ExecuteNonQuery(query, parameters);

                Console.WriteLine("Данные успешно добавлены");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении данных: {ex.Message}");
            }
        }
    }
}
