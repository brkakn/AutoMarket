namespace AutoMarket.Api.Features.ShoppingCart.Models
{
    public class AddItemRequestModel
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
