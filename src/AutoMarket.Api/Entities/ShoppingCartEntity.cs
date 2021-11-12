using AutoMarket.Api.Entities.Common;
using System.Collections.Generic;

namespace AutoMarket.Api.Entities
{
    public class ShoppingCartEntity : BaseEntity
    {
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public ICollection<ShoppingCartDetailEntity> ShoppingCartDetails { get; set; }
    }
}
