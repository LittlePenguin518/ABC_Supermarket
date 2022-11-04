using Supermarket.Model;

namespace Supermarket
{
   
    public class Utilities : IUtilities
    {
        public Stock GetListOfItemAvailable()
        {
            Stock ItemsForSales = new Stock()
            {
                Products = new List<Product>
                {
                    new()
                    {
                        ProductCode = "FR1",
                        ProductName= "Fruit tea",
                        ProductPrice=3.11
                    },
                      new()
                    {
                        ProductCode = "SR1",
                        ProductName= "Strawberries",
                        ProductPrice=5.00
                    },
                      new()
                    {
                        ProductCode = "CF1",
                        ProductName= "Coffee",
                        ProductPrice=11.23
                    }
                }
            };
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

        public double checkDiscount(Receipt receipt)
        {
            double totalDiscount = 0;

            List<ItemBought> itemsBought = receipt.ItemsBought;
            totalDiscount = ApplyBuyOneGetOneFree(itemsBought) + ApplyBuyThreeOrMore(itemsBought);

            return totalDiscount;
        }

        public double ApplyBuyOneGetOneFree(List<ItemBought> itemsBought)
        {

            var fruitTeaBought = itemsBought.FindAll(x => x.Item.ProductCode == "FR1").ToList();
            int fruitTeaBoughtTotal = (int)fruitTeaBought.Sum(x => x.Quantity);
            double discountFruitTea = fruitTeaBoughtTotal - (fruitTeaBoughtTotal % 2);
            discountFruitTea = (discountFruitTea / 2) * fruitTeaBought[0].Item.ProductPrice;

            return discountFruitTea;
        }

        public double ApplyBuyThreeOrMore(List<ItemBought> itemsBought)
        {
            var strawberriesBought = itemsBought.FindAll(x => x.Item.ProductCode == "SR1").ToList();
            double discountStrawberries = 0;
            if (strawberriesBought.Sum(x => x.Quantity) >= 3)
            {
                discountStrawberries = strawberriesBought.Sum(x => x.Quantity) * .50;
            }

            return discountStrawberries;
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
                    if (stock.Products.Where(x => x.ProductCode == inputProductCode).ToList().Count > 0)
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

                        Console.WriteLine("Your total purchase before any discount £" + receipt.Subtotal);
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

            receipt.Discount = checkDiscount(receipt);
            Console.WriteLine("Your total transaction is £" + receipt.TotalTransaction);
            Console.WriteLine("Please enter your payment");
            paymentReceived = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Here is your change: £" + Math.Round((paymentReceived - receipt.TotalTransaction), 2));
            Console.WriteLine("Thank you for shopping with us\n Please call again\n\n");

        }


    }
}
