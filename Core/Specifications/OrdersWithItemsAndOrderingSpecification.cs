using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification : SpecificationBase<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddIncludes();
            AddDescendingOrder();
        }

        public OrdersWithItemsAndOrderingSpecification(int orderId, string email) : base(o => o.Id == orderId && o.BuyerEmail == email)
        {
            AddIncludes();
        }

        private void AddIncludes() 
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        private void AddDescendingOrder()
        {
            AddOrderByDescending(o => o.OrderDate);
        }
    }
}