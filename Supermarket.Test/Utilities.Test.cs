using Supermarket.Model;
using System.ComponentModel;

namespace Supermarket.Test
{
    public class UtilitiesTest
    {
        private IUtilities _utilities = new Utilities();
        Stock product;
        Receipt receipt;
        List<ItemBought> itemsBought;

        [SetUp]
        public void Setup()
        {
            product = new Stock()
            {
                Products = new List<Product>
                {
                    new()
                    {
                        ProductCode = "Test123",
                        ProductName= "TestItem",
                        ProductPrice=123
                    }
                }
            };

            itemsBought = new List<ItemBought>()
                {
                    new()
                    {
                        Item = new()
                        {
                             ProductCode = "FR1",
                             ProductName= "Fruit tea",
                             ProductPrice=3.11
                        },
                        Quantity = 6

                    },
                    new()
                    {
                        Item = new()
                        {
                            ProductCode = "SR1",
                            ProductName= "Strawberries",
                            ProductPrice=5.00
                        },
                        Quantity = 9

                    },

                };


            receipt = new Receipt()
            {
                ItemsBought = new List<ItemBought>()
                {
                    new()
                    {
                        Item = new()
                        {
                             ProductCode = "FR1",
                             ProductName= "Fruit tea",
                             ProductPrice=3.11
                        },
                        Quantity = 3

                    },
                    new()
                    {
                        Item = new()
                        {
                            ProductCode = "SR1",
                            ProductName= "Strawberries",
                            ProductPrice=5.00
                        },
                        Quantity = 3

                    },

                },
                
            };
        }

        [Test]
        public void ShouldPrintAllStock()
        {
            _utilities.PrintStock();
            Assert.That(product.Products.Count, Is.EqualTo(1));

        }

        [Test]
        public void ShouldPrintMainMenu()
        {
            _utilities.PrintMainMenu();

        }

        [Test]
        public void ShouldGetListOfItemAvailable()
        {
            Stock result= _utilities.GetListOfItemAvailable();

            Assert.That(result.Products.Count, Is.EqualTo(3));

        }


        [Test]
        public void ShouldCheckDiscount()
        {
            Double result = _utilities.checkDiscount(receipt);

            Assert.That(Math.Round(result,2), Is.EqualTo(4.61));

        }

        [Test]
        public void ShouldApplyBuyOneGetOneFree()
        {
            Double result = _utilities.ApplyBuyOneGetOneFree(itemsBought);

            Assert.That(Math.Round(result, 2), Is.EqualTo(9.33));

        }

        [Test]
        public void ShouldApplyBuyThreeOrMore()
        {
            Double result = _utilities.ApplyBuyThreeOrMore(itemsBought);

            Assert.That(Math.Round(result, 2), Is.EqualTo(4.50));

        }

 

    }
}