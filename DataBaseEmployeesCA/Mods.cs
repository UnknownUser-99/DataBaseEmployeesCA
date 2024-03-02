using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEmployeesCA
{
    class Mods
    {
        public enum AppMods
        {
            Mode1 = 1,
            Mode2 = 2,
            Mode3 = 3,
            Mode4 = 4,
            Mode5 = 5,
            Mode6 = 6,
        }

        public void PerformMode(AppMods mode)
        {
            switch (mode)
            {
                case AppMods.Mode1:
                    Mode1();
                    break;
                case AppMods.Mode2:
                    Mode2();
                    break;
                case AppMods.Mode3:
                    Mode3();
                    break;
                case AppMods.Mode4:
                    Mode4();
                    break;
                case AppMods.Mode5:
                    Mode5();
                    break;
                default:
                    Console.WriteLine("Неверный режим");
                    break;
            }
        }

        private void Mode1()
        {
            Mode1 mode = new Mode1();
            mode.Launch();
        }

        private void Mode2()
        {
            Mode2 mode = new Mode2();
            mode.Launch();
        }

        private void Mode3()
        {
            Mode3 mode = new Mode3();
            mode.Launch();
        }

        private void Mode4()
        {
            Mode4 mode = new Mode4();
            mode.Launch();
        }

        private void Mode5()
        {
            Mode5 mode = new Mode5();
            mode.Launch();
        }
    }
}
