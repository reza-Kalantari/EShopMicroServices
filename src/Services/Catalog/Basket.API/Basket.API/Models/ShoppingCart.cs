using Marten.Schema;

namespace Basket.API.Models
{
    public class ShoppingCart
    {
        [Identity]
        public string Username { get; set; } = default;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal totalPrice => Items.Sum(item => item.Price);

        public ShoppingCart(string username)
        {
            Username = username;
        }
        public ShoppingCart()
        {
            
        }
    }
}
