using AutoMarket.Api.Entities;
using AutoMarket.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoMarket.Api.Test.Entities
{
    public class CartEntityTests
    {
        [Theory, InlineData(1), InlineData(-1)]
        public void AddAmount_ShouldAssertValidModel_QuantityAndPrice(decimal amount)
        {
            //Arrange
            var shoppingCartEntity = new ShoppingCartEntity()
            {
                Amount = 100
            };

            var expectedValue = shoppingCartEntity.Amount + amount;

            //Act
            shoppingCartEntity.AddAmount(amount);

            //Assert
            Assert.NotNull(shoppingCartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, shoppingCartEntity.Status);
            Assert.Equal(expectedValue, shoppingCartEntity.Amount);
        }

        [Fact]
        public void Add_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartEntity = new ShoppingCartEntity();

            //Act
            cartEntity.Add();

            //Assert
            Assert.NotEqual(DateTime.MinValue, cartEntity.CreateDate);
            Assert.Null(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartEntity.Status);
        }

        [Fact]
        public void Update_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartEntity = new ShoppingCartEntity();

            //Act
            cartEntity.Update();

            //Assert
            Assert.NotNull(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartEntity.Status);
        }

        [Fact]
        public void Delete_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartEntity = new ShoppingCartEntity();

            //Act
            cartEntity.Delete();

            //Assert
            Assert.NotNull(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.PASSIVE, cartEntity.Status);
        }
    }
}
