using AutoMapper;

namespace AutoMarket.Api.Infrastructures.Mapper
{
    public interface IMapping
    {
        void CreateMappings(IProfileExpression profileExpression);
    }
}
