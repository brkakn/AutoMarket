using AutoMarket.Api.Entities;
using AutoMarket.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMarket.Api.Infrastructures.Database
{
    public class SeedDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AutoMarketDbContext(serviceProvider.GetRequiredService<DbContextOptions<AutoMarketDbContext>>()))
            {
                if(!context.Users.Any())
                {
                    var user = new UserEntity()
                    {
                        Name = "Burak",
                        Surname = "Akın",
                        Email = "akinburak94@gmail.com",
                        UserName = "admin",
                        Password = SecurityHelper.ComputeSha256Hash("asd123")
                    };
                    user.Add();
                    context.Users.Add(user);
                }

                List<ItemEntity> itemList = new();
                if(!context.Items.Any())
                {
                    var item = new ItemEntity()
                    {
                        Name = "225/45 R17 Kış Lastiği",
                        Description = "225 45 R17 H XL \nYakıt Verimliliği:D\nIslak Zeminde Frenleme:A",
                        Code = "2254517W",
                        Price = 600
                    };
                    item.Add();
                    context.Items.Add(item);
                    itemList.Add(item);

                    var item1 = new ItemEntity()
                    {
                        Name = "Astra K Silecek",
                        Description = "2015 ve sonrası Astra için uyumlu silecek",
                        Code = "SLCKASTRA",
                        Price = 80
                    };
                    item1.Add();
                    context.Items.Add(item1);
                    itemList.Add(item1);

                    var item2 = new ItemEntity()
                    {
                        Name = "Motul 5W30 Dexos1 Gen2 Motor Yağı",
                        Description = "Dexos1 Gen2 LSPI sorunu için özel üretilmiştir.",
                        Code = "MTL530DXS1",
                        Price = 300
                    };
                    item2.Add();
                    context.Items.Add(item2);
                    itemList.Add(item2);
                }

                if(!context.Stocks.Any())
                {
                    var stock = new StockEntity()
                    {
                        Item = itemList[0],
                        FreeQuantity = 4,
                        Quantity = 8,
                    };
                    stock.Add();
                    context.Stocks.Add(stock);

                    var stock1 = new StockEntity()
                    {
                        Item = itemList[1],
                        FreeQuantity = 1,
                        Quantity = 1
                    };
                    stock1.Add();
                    context.Stocks.Add(stock1);

                    var stock2 = new StockEntity()
                    {
                        Item = itemList[2],
                        FreeQuantity = 3,
                        Quantity = 4
                    };
                    stock2.Add();
                    context.Stocks.Add(stock2);
                }

                context.SaveChanges();
            }
        }
    }
}
