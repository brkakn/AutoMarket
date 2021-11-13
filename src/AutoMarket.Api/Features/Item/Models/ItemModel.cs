using AutoMapper;
using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Mapper;

namespace AutoMarket.Api.Features.Item.Models
{
    public class ItemModel : IMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<ItemEntity, ItemModel>();
        }
    }
}
