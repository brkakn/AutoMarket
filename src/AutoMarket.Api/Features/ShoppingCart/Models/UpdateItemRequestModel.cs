namespace AutoMarket.Api.Features.ShoppingCart.Models
{
    public class UpdateItemRequestModel
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
