namespace Supermarket.Model
{
    public class Receipt
    {
        public List<ItemBought> ItemsBought { get; set; }
        public double Discount { get; set; }
        public double Subtotal => Math.Round(ItemsBought.Sum(x => x.Amount), 2);
        public double TotalTransaction => Subtotal - Discount;


    }
}
