
namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }
        private OrderItemId(Guid value) => Value = value;

        public static OrderItemId Of(Guid orderId)
        {
            ArgumentNullException.ThrowIfNull(orderId);
            if (orderId == Guid.Empty)
            {
                throw new DomainException("OrderItemId can not be empty");
            }
            return new OrderItemId(orderId);
        }
    }
}
    