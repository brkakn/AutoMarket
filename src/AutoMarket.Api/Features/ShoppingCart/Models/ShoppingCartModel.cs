using AutoMapper;
using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Mapper;
using System.Collections.Generic;

namespace AutoMarket.Api.Features.ShoppingCart.Models
{
    public class ShoppingCartModel : IMapping
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public List<ShoppingCartDetailsModel> Details { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression
                .CreateMap<ShoppingCartEntity, ShoppingCartModel>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom((src, dest) =>
                {
                    return src.ShoppingCartDetails;
                }));
        }
    }
}
