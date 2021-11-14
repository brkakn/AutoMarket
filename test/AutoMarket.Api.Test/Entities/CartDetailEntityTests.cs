using AutoMarket.Api.Entities;
using AutoMarket.Api.Enums;
using System;
using Xunit;

namespace AutoMarket.Api.Test.Entities
{
    public class CartDetailEntityTests
    {
        [Theory, InlineData(1), InlineData(-1)]
        public void AddQuantity_ShouldAssertValidModel_Quantity(int quantity)
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };
            var expectedValue = cartDetailEntity.Quantity + quantity;
            //Act
            cartDetailEntity.AddQuantity(quantity);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Quantity);
            Assert.Equal(expectedValue, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.ShoppingCart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.ShoppingCart.Status);
            Assert.Equal(expectedValue, cartDetailEntity.ShoppingCart.Amount);
        }

        [Theory, InlineData(2), InlineData(0)]
        public void ChangeQuantity_ShouldAssertValidModel_Quantity(int quantity)
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.ChangeQuantity(quantity);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(quantity, cartDetailEntity.Quantity);
            Assert.Equal(quantity, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.ShoppingCart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.ShoppingCart.Status);
            Assert.Equal(quantity, cartDetailEntity.ShoppingCart.Amount);
        }

        [Theory, InlineData(1), InlineData(-1)]
        public void AddAmount_ShouldAssertValidModel_Quantity(int quantity)
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };
            var expectedValue = quantity + cartDetailEntity.Quantity;
            //Act
            cartDetailEntity.AddAmount(quantity);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.ShoppingCart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.ShoppingCart.Status);
            Assert.Equal(expectedValue, cartDetailEntity.ShoppingCart.Amount);
        }

        [Theory, InlineData(1, 10), InlineData(-1, 10)]
        public void AddAmount_ShouldAssertValidModel_QuantityAndPrice(int quantity, decimal price)
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };
            var expectedValue = quantity * price + cartDetailEntity.Quantity;

            //Act
            cartDetailEntity.AddAmount(quantity, price);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.ShoppingCart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.ShoppingCart.Status);
            Assert.Equal(expectedValue, cartDetailEntity.ShoppingCart.Amount);
        }

        [Fact]
        public void Add_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.Add();

            //Assert
            Assert.NotEqual(DateTime.MinValue, cartDetailEntity.CreateDate);
            Assert.Null(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
        }

        [Fact]
        public void Update_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.Update();

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
        }

        [Fact]
        public void Delete_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartDetailEntity = new ShoppingCartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                ShoppingCart = new ShoppingCartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.Delete();

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.PASSIVE, cartDetailEntity.Status);
        }
    }
}
