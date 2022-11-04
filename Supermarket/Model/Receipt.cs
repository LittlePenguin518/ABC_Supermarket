namespace Supermarket.Model
{
    public class Receipt
    {
        public List<ItemBought> ItemsBought { get; set; }
        public double totalDiscount => Math.Round(ApplyBuyOneGetOneFree(ItemsBought) + ApplyBuyThreeOrMore(ItemsBought),2);
        public double Subtotal => Math.Round(ItemsBought.Sum(x => x.Amount), 2);
        public double TotalTransaction => Math.Round(Subtotal - totalDiscount,2);

        public double ApplyBuyOneGetOneFree(List<ItemBought> itemsBought)
        {
            double discountFruitTea = 0;

            var fruitTeaBought = itemsBought.FindAll(x => x.Item.ProductCode == "FR1").ToList();
            
            if (fruitTeaBought.Count > 0)
            {
                int fruitTeaBoughtTotal = (int)fruitTeaBought.Sum(x => x.Quantity);
                discountFruitTea = fruitTeaBoughtTotal - (fruitTeaBoughtTotal % 2);
                discountFruitTea = (discountFruitTea / 2) * fruitTeaBought[0].Item.ProductPrice;
            }
            return discountFruitTea;
        }

        public double ApplyBuyThreeOrMore(List<ItemBought> itemsBought)
        {
            double discountStrawberries = 0;
            var strawberriesBought = itemsBought.FindAll(x => x.Item.ProductCode == "SR1").ToList();
            if (strawberriesBought.Count > 0)
            {
                
                if (strawberriesBought.Sum(x => x.Quantity) >= 3)
                {
                    discountStrawberries = strawberriesBought.Sum(x => x.Quantity) * .50;
                }
            }
            return discountStrawberries;
        }
    }
}
