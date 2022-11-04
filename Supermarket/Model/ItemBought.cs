namespace Supermarket.Model
{
    public class ItemBought
    {
        public Product Item { get; set; }
        public int Quantity { get; set; }
        public double Amount => Math.Round(Convert.ToDouble(Quantity) * Item.ProductPrice, 2);


    }
}
