using System;
using System.Collections.Generic;
using System.Text;
using FoodParty.Validators;
using Xunit;

namespace FoodParty.Tests
{
    public class CompareViewModelTests
    {
        [Fact]
        public void PriceValidator_GivenValidPrice_ReturnsTrue() {
            // Arrange
            decimal validPrice = 10M;

            // Act
            var result = Validator.ValidatePrice(validPrice);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void PriceValidator_GivenInvalidPrice_Negative_ReturnsFalse() {
            // Arrange
            decimal invalidPrice = -10M;

            // Act
            var result = Validator.ValidatePrice(invalidPrice);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PriceValidator_GivenInvalidPrice_MoreDigits_ReturnsFalse() {
            // Arrange
            decimal invalidPrice = 2.1234M;

            // Act
            var result = Validator.ValidatePrice(invalidPrice);

            // Assert
            Assert.False(result);
        }
    }
}
