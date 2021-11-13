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
        [ForeignKey("ItemId")]
        public ItemEntity Item { get; set; }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
            ShoppingCart.AddAmount(quantity * Item.Price);
            Update();
        }
        public void ChangeQuantity(int quantity)
        {
            ShoppingCart.AddAmount((quantity - Quantity) * Item.Price);
            Quantity = quantity;
            Update();
        }
    }
}
