using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SydneyHotel
{
    class Program
    {
        class ReservationDetail
        {
            public string customerName { get; set; }
            public int nights { get; set; }
            public string roomService { get; set; }
            public double totalPrice { get; set; }
        }

        // calculation of room services
        static double Price(int night, string isRoomService)
        {
            if (night < 1 || night > 20)
                throw new ArgumentOutOfRangeException(nameof(night), "Nights must be between 1 and 20.");

            double price = 0;
            if (night <= 3)
                price = 100 * night;
            else if (night <= 10)
                price = 80.5 * night;
            else // night <= 20
                price = 75.3 * night;

            // case-insensitive comparison for "yes"
            if (string.Equals(isRoomService, "yes", StringComparison.OrdinalIgnoreCase))
                return price + price * 0.1;

            return price;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(".................Welcome to Sydney Hotel...............");
            Console.Write("\nEnter no. of Customer: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n--------------------------------------------------------------------\n");

            ReservationDetail[] rd = new ReservationDetail[n];

            for (int i = 0; i < n; i++)
            {
                rd[i] = new ReservationDetail();

                Console.Write("Enter customer name: ");
                rd[i].customerName = Console.ReadLine();

                // (1) FIXED: robust validation loop for nights (1–20)
                while (true)
                {
                    Console.Write("Enter the number of nights (1–20): ");
                    int nightsInput;
                    if (int.TryParse(Console.ReadLine(), out nightsInput) && nightsInput >= 1 && nightsInput <= 20)
                    {
                        rd[i].nights = nightsInput;
                        break;
                    }
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 20.");
                }

                Console.Write("Enter yes/no to indicate whether you want room service: ");
                rd[i].roomService = Console.ReadLine();

                // (2) FIXED: Price now validates nights and compares roomService safely
                rd[i].totalPrice = Price(rd[i].nights, rd[i].roomService);

                Console.WriteLine($"The total price from {rd[i].customerName} is ${rd[i].totalPrice}");
                Console.WriteLine("\n--------------------------------------------------------------------");
            }

            var (minPrice, minindex) = rd.Select(x => x.totalPrice).Select((m, i) => (m, i)).Min();
            var (maxPrice, maxindex) = rd.Select(x => x.totalPrice).Select((m, i) => (m, i)).Max();

            ReservationDetail maxrd = rd[maxindex];
            ReservationDetail minrd = rd[minindex];

            Console.WriteLine("\n\t\t\t\tSummary of reservation");
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("Name\t\tNumber of nights\t\tRoom service\t\tCharge");
            Console.WriteLine($"{minrd.customerName}\t\t\t{minrd.nights}\t\t\t{minrd.roomService}\t\t\t{minrd.totalPrice}");
            Console.WriteLine($"{maxrd.customerName}\t\t{maxrd.nights}\t\t\t{maxrd.roomService}\t\t\t{maxrd.totalPrice}");
            Console.WriteLine("\n--------------------------------------------------------------------\n");
            Console.WriteLine($"The customer spending most is {maxrd.customerName} ${maxrd.totalPrice}");
            Console.WriteLine($"The customer spending least is {minrd.customerName} ${minrd.totalPrice}");
            Console.WriteLine("Press any key to continue....");
            Console.ReadLine();
        }
    }
}
