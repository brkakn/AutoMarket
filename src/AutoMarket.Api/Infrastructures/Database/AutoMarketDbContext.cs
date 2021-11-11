using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AutoMarket.Api.Infrastructures.Database
{
    public class AutoMarketDbContext : DbContext
    {
        public AutoMarketDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
    }
}
