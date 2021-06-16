using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPark
{
    class Program
    {
        static void Main(string[] args)
        {
            var Park = new List<Bus>();  // список автобусов в парке  
            var Route = new List<Bus>(); // список автобусов в пути
            while (true)
            {
                switch (Select())
                {
                    case 1:  // регистрация автобусов
                        Console.WriteLine("Введите колличество регистрируемых автобусов: ");
                        int CountBus = Convert.ToInt32(Console.ReadLine());
                        for (int i = 1; i <= CountBus; i++)
                        {
                            CreateBusPark();
                        }
                        break;

                    case 2:  // автобус выезжает на маршрут
                        Console.WriteLine("\nВведите номер автобуса: ");
                        BusLeft(Console.ReadLine());
                        break;

                    case 3: // автобус заезжает в парк
                        Console.WriteLine("\nВведите номер автобуса: ");
                        BusArrived(Console.ReadLine());
                        break;

                    case 4:  // список автобусов
                        Console.WriteLine("\nАвтобусы в парке: ");
                        foreach (Bus B in Park)
                        {
                            Console.WriteLine(B.Number + " " + B.Driver + " " + B.Destination);
                        }

                        Console.WriteLine("\nАвтобусы на маршруте: ");
                        foreach (Bus B in Route)
                        {
                            Console.WriteLine(B.Number + " " + B.Driver + " " + B.Destination); 
                        }
                        break;

                    default:  // неверный ввод
                        Console.WriteLine("Введите 1-4\n");
                        break;
                }
            }
            #region Methods
            int Select()  // юзер выбирает кейс
            {
                Console.WriteLine("1 - добавить автобусы\n2 - автобус выехал на маршрут\n3 - автобус въехал в парк\n4 - список автобусов\n");
                return Convert.ToInt32(Console.ReadLine());
            }

            void CreateBusPark() // формирование списка автобусов в парке
            {   

                Console.WriteLine("Введите номер автобуса: ");
                string Number = Console.ReadLine();
                Console.WriteLine("Введите фамилию и инициалы водителя: ");
                string Driver = Console.ReadLine();
                Console.WriteLine("Введите номер маршрута: ");
                string Destination = Console.ReadLine();
                Bus bus = new Bus(Number, Driver, Destination);
                Park.Add(bus);
            }

            void BusLeft(string number)  // выезд автобусов
            {
                foreach (Bus B in Park)
                {
                    if (B.Number == number)
                    {
                        Route.Add(B);
                        Park.Remove(B);
                        return;
                    }
                }
            }

            void BusArrived(string number)  // въезд автобусов
            {
                foreach (Bus B in Route)
                {
                    if(B.Number == number)
                    {
                        Park.Add(B);
                        Route.Remove(B);
                        return;
                    }
                }
            }
            #endregion
        }
    }
}

// BUS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPark
{
     class Bus
    {
        public string Number { get; set; }
        public string Driver { get; set; }
        public string Destination { get; set; }

        public Bus(string number, string driver, string destination) 
        {
            Number = number;
            Driver = driver;
            Destination = destination;
        }
    }
}
