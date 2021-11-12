using AutoMarket.Api.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Api.Entities
{
    public class ShoppingCartDetailEntity : BaseEntity
    {
        public long ShoppingCartId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ShoppingCartId")]
        public ShoppingCartEntity ShoppingCart { get; set; }
    }
}
