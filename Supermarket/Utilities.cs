using Newtonsoft.Json;
using Supermarket.Model;

namespace Supermarket
{
   
    public class Utilities : IUtilities
    {
        public Stock GetListOfItemAvailable()
        {
            Stock ItemsForSales = new Stock();
            Product productToAdd=new Product();
            List<Product> products = new List<Product>();

            foreach (Product p in JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(@"products.json")))
            {
                products.Add(p);
            }

            ItemsForSales.Products=products;
            return ItemsForSales;
        }

        public void PrintStock()
        {
            Stock stock = GetListOfItemAvailable();
            for (int i = 0; i < stock.Products.Count; i++)
            {
                Console.WriteLine("     Product Code " + stock.Products[i].ProductCode);
                Console.WriteLine("     Product Name " + stock.Products[i].ProductName);
                Console.WriteLine("     Unit Price £" + stock.Products[i].ProductPrice + "\n");
            }

        }
        public void PrintMainMenu()
        {
            Console.WriteLine("---Main Menu ---\n");
            Console.WriteLine("Press 1 to view list of items available\n");
            Console.WriteLine("Press 2 for checkout\n");
            Console.WriteLine("Press 3 for exit\n");
        }

        public void Checkout(string inputProductCode)
        {   

            Stock stock = GetListOfItemAvailable();
            int count = 0;
            int quantity = 0;
            double paymentReceived;
            Receipt receipt = new Receipt();
            ItemBought itemBought;
            List<ItemBought> itemsBought = new List<ItemBought>();

            while (inputProductCode != "0")
            {
                itemBought = new ItemBought();
                Console.WriteLine("\nPlease enter Product Code or press 0 if you finish with the checkout");
                inputProductCode = Console.ReadLine();

                if (inputProductCode != "0")
                {
                    if (stock.Products.Select(x => x.ProductCode == inputProductCode).ToList().Count > 0)
                    {
                        Product itemSelected = stock.Products.Where(x => x.ProductCode == inputProductCode).First();

                        Console.WriteLine("Product Code " + inputProductCode);
                        Console.WriteLine("Product Name " + itemSelected.ProductName);
                        Console.WriteLine("Unit Price £" + itemSelected.ProductPrice);

                        Console.WriteLine("Please enter quantity");
                        quantity = Convert.ToInt32(Console.ReadLine());

                        itemBought.Item = itemSelected;
                        itemBought.Quantity = quantity;

                        Console.WriteLine("Amount is £" + itemBought.Amount);
                        itemsBought.Add(itemBought);
                        receipt.ItemsBought = itemsBought;

                        Console.WriteLine("Total purchase before any discount £" + receipt.Subtotal);
                        Console.WriteLine("Total purchase after discount £" + receipt.TotalTransaction);

                        if(receipt.totalDiscount!=0) Console.WriteLine("Discount applied £" + receipt.totalDiscount);
                    }
                    else
                    {
                        Console.WriteLine("Product not found");
                    }
                }
                else if (inputProductCode == "0" && count == 0)
                {
                    return;
                }
                count++;
            }

            Console.WriteLine("Total purchase to pay £" + receipt.TotalTransaction);
            Console.WriteLine("Please enter your payment");
            paymentReceived = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Here is your change: £" + Math.Round((paymentReceived - receipt.TotalTransaction), 2));
            Console.WriteLine("Thank you for shopping with us\n Please call again\n\n");

        }
    }
}
