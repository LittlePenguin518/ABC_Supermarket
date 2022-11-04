using Supermarket.Model;

namespace Supermarket
{
    public interface IUtilities
    {

        Stock GetListOfItemAvailable();
        void PrintStock();
        void PrintMainMenu();
        double checkDiscount(Receipt receipt);
        void Checkout(string inputProductCode="");
        double ApplyBuyOneGetOneFree(List<ItemBought> itemsBought);
        double ApplyBuyThreeOrMore(List<ItemBought> itemsBought);


    }
}
