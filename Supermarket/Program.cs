using Supermarket.Model;

namespace Supermarket
{
    public class Program
    {

        public static void Main(string[] args)
        {
            int Choice;
            Utilities checkout = new Utilities();

            Console.WriteLine("----- Welcome to ABC Supermarket -----\n\n");

            checkout.PrintMainMenu();

            Console.WriteLine("Please enter your option");
            Choice = Convert.ToInt32(Console.ReadLine());
            int count = 1;
            Stock stock = checkout.GetListOfItemAvailable();

            while (Choice != 3)
            {
                if (count != 1)
                {
                    checkout.PrintMainMenu();
                    Console.WriteLine("Please enter your option");
                    Choice = Convert.ToInt32(Console.ReadLine());
                }

                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("Below is the list of product available in stock\n\n");
                        checkout.PrintStock();
                        break;

                    case 2:
                        checkout.Checkout("");
                        break;

                    case 3:
                        break;
                }

                count++;
            }
        }

    }
}