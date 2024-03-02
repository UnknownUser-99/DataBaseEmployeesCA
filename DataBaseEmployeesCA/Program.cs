using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEmployeesCA
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Mods.AppMods Mods;

                Console.WriteLine("Выберите режим работы:");

                if (Enum.TryParse(Console.ReadLine(), out Mods) && Enum.IsDefined(typeof(Mods.AppMods), Mods))
                {
                    Mods mods = new Mods();
                    mods.PerformMode(Mods);
                }
                else
                {
                    Console.WriteLine("Режим работы выбран неверно");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
