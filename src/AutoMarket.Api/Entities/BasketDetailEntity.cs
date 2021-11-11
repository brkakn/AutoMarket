using AutoMarket.Api.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Api.Entities
{
    public class BasketDetailEntity : BaseEntity
    {
        public long BasketId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("BasketId")]
        public BasketEntity Basket { get; set; }
    }
}
