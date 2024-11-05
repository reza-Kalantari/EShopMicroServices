
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Modles
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderitems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderitems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShipingAddres { get; private set; } = default!;
        public Address BillingAddres { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotlaPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShipingAddres = shippingAddress,
                BillingAddres = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending 
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update (OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShipingAddres = shippingAddress;
            BillingAddres = billingAddress;
            Payment = payment;
            Status = OrderStatus.Pending;
            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price) 
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id,productId, quantity, price);
            _orderitems.Add(orderItem);
        }

        public void remove(ProductId productId)
        {
            var orderItem = _orderitems.FirstOrDefault(i => i.ProductId == productId);
            if (orderItem is not null) { 
                _orderitems.Remove(orderItem);
            }
        }



    }
}
