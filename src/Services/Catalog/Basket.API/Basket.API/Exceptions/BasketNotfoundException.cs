using BuildingBlocks.Exceptions;

namespace Basket.API.Exceptions
{
    internal class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string username) : base("Basket", username)
        {
        }
    }
}
