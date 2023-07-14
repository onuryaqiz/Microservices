using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //EF Core Features Not:EF Core yerine HyperNet kullansaydık ona özgü feature'ları kullanacaktık.
    // --Owned Types
    // --Shadow Property
    // --Backing Field

    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreateDate { get; private set; }

        public Address Address { get; private set; } //EF Core Owned Entity Types'a bakılabilir.

        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems; //field üzerinden gerçekleştiriyoruz.Backing Fields'a bakılabilir.

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems; //private yapılan orderItems'ı sadece Read Only yaparak dış dünyaya açtık. Ve data eklenmesini engelledik.

        public Order(Address address, string buyerId)
        {
            _orderItems = new List<OrderItem>();
            CreateDate= DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }

        public void AddOrderItem(string productId,string productName,decimal price,string pictureUrl) 
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct) 
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);

                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
