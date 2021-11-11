using AutoMarket.Api.Entities.Common;
using AutoMarket.Api.Enums;
using System.Collections.Generic;

namespace AutoMarket.Api.Entities
{
    public class BasketEntity : BaseEntity
    {
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public CurrencyEnum Currency { get; set; }
        public ICollection<BasketDetailEntity> BasketDetails { get; set; }
    }
}
