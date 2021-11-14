using AutoMarket.Api.Entities.Common;
using System.Collections.Generic;

namespace AutoMarket.Api.Entities
{
    public class ItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public ICollection<StockEntity> Stocks { get; set; }
        public ICollection<ShoppingCartDetailEntity> ShoppingCartDetails { get; set; }
    }
}
