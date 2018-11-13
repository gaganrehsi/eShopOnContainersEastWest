using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateItemPriceInEast
{
    public class ProductPriceChangedIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }
        public int ProductId { get; private set; }
        public decimal NewPrice { get; private set; }
        public decimal OldPrice { get; private set; }

        public ProductPriceChangedIntegrationEvent(int productId, decimal newPrice, decimal oldPrice)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}