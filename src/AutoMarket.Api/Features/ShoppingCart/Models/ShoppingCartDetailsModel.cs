using AutoMapper;
using AutoMarket.Api.Entities;
using AutoMarket.Api.Features.Item.Models;
using AutoMarket.Api.Infrastructures.Mapper;

namespace AutoMarket.Api.Features.ShoppingCart.Models
{
    public class ShoppingCartDetailsModel : IMapping
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public ItemModel Item { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<ShoppingCartDetailEntity, ShoppingCartDetailsModel>();
        }
    }
}
