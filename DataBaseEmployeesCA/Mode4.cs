using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bogus;

namespace DataBaseEmployeesCA
{
    class Mode4
    {
        private const int BatchSize = 1000;
        private const int TotalRecords = 1000000;
        private const int SpecialRecords = 100;

        private Employee[] EmployeesRandom = new Employee[TotalRecords];
        private Employee[] EmployeesSpecial = new Employee[SpecialRecords];

        private string TableName = "Employees";

        public void Launch()
        {
            GenerateRandom();

            GenerateSpecial();

            AddToDatabase();

        }

        private void GenerateRandom()
        {
            try
            {
                var genderRandom = new Random();

                var faker = new Bogus.Faker();

                for (int i = 0; i < TotalRecords; i++)
                {
                    var gender = genderRandom.Next(2) == 0 ? "male" : "female";
                    var fullName = $"{faker.Name.LastName()} {(gender == "male" ? faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male) : faker.Name.FirstName(Bogus.DataSets.Name.Gender.Female))} {faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male)}";
                    var birthDate = faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2000, 12, 31)).ToString("yyyy-MM-dd");

                    EmployeesRandom[i] = new Employee(fullName, birthDate, gender);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации данных: {ex.Message}");
            }
        }

        private void GenerateSpecial()
        {
            try
            {
                var faker = new Bogus.Faker();

                for (int i = 0; i < SpecialRecords; i++)
                {
                    var lastName = faker.Name.LastName();

                    while (!lastName.StartsWith("F"))
                    {
                        lastName = faker.Name.LastName();
                    }

                    var firstName = faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                    var middleName = faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                    var fullName = $"{lastName} {firstName} {middleName}";
                    var birthDate = faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2000, 12, 31)).ToString("yyyy-MM-dd");

                    EmployeesSpecial[i] = new Employee(fullName, birthDate, "male");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации данных: {ex.Message}");
            }
        }

        private void AddToDatabase()
        {
            Database DB = new Database();

            DB.BatchInsertData(TableName, ConvertToDataTable(EmployeesRandom), BatchSize);

            DB.BatchInsertData(TableName, ConvertToDataTable(EmployeesSpecial));
        }

        private DataTable ConvertToDataTable(Employee[] employees)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("fullName", typeof(string));
            dataTable.Columns.Add("birthDate", typeof(string));
            dataTable.Columns.Add("gender", typeof(string));

            foreach (var employee in employees)
            {
                DataRow row = dataTable.NewRow();
                row["fullName"] = employee.FullName;
                row["birthDate"] = employee.BirthDate;
                row["gender"] = employee.Gender;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
