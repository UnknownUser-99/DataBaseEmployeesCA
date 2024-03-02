using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEmployeesCA
{
    class Mode2
    {
        public void Launch()
        {
            string Fullname;
            string inputDate;
            string BirthDate;
            string Gender;

            Console.WriteLine("Поочерёдно введите полное имя, дату рождения и пол:");
            Fullname = Console.ReadLine();

            inputDate = Console.ReadLine();

            if (!IsDateValid(inputDate, out BirthDate))
            {
                Console.WriteLine("Ошибка ввода даты");
                return;
            }

            Gender = Console.ReadLine();

            if (!IsGenderValid(Gender))
            {
                Console.WriteLine("Ошибка ввода пола");
                return;
            }

            Employee employee = new Employee(Fullname, BirthDate, Gender);

            employee.AddToDatabase();
        }

        private bool IsGenderValid(string inputGender)
        {
            return inputGender == "male" || inputGender == "female";
        }

        private bool IsDateValid(string inputDate, out string result)
        {
            if (DateTime.TryParseExact(inputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                result = parsedDate.ToString("yyyy-MM-dd");
                return true;
            }

            result = null;
            return false;
        }
    }
}
