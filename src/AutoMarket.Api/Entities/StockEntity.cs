using AutoMarket.Api.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Api.Entities
{
    public class StockEntity : BaseEntity
    {
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public long FreeQuantity { get; set; }
        [ForeignKey("ItemId")]
        public ItemEntity Item { get; set; }
    }
}
