using Supermarket.Model;

namespace Supermarket
{
    public interface IUtilities
    {

        Stock GetListOfItemAvailable();
        void PrintStock();
        void PrintMainMenu();
        void Checkout(string inputProductCode="");
        


    }
}
