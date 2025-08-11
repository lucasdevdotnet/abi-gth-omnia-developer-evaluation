using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_CustomerId_Is_Empty()
        {
            var sale = SaleTestData.GenerateValidSale();
            sale.CustomerId = Guid.Empty;
            var result = _validator.TestValidate(sale);
            result.ShouldHaveValidationErrorFor(s => s.CustomerId);
        }

        [Fact]
        public void Should_Have_Error_When_SaleDate_Is_Future()
        {
            var sale = SaleTestData.GenerateValidSale();
            sale.SaleDate = DateTime.UtcNow.AddDays(1);
            var result = _validator.TestValidate(sale);
            result.ShouldHaveValidationErrorFor(s => s.SaleDate);
        }

        [Fact]
        public void Should_Have_Error_When_TotalAmount_Is_Zero()
        {
            var sale = SaleTestData.GenerateValidSale();
            var itemsField = typeof(Sale).GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var itemsList = (List<SaleItem>)itemsField.GetValue(sale);
            itemsList.Clear();
            var result = _validator.TestValidate(sale);
            result.ShouldHaveValidationErrorFor(s => s.TotalAmount);
        }

        [Fact]
        public void Should_Have_Error_When_Items_Is_Empty()
        {
            var sale = SaleTestData.GenerateValidSale();
            var itemsField = typeof(Sale).GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var itemsList = (List<SaleItem>)itemsField.GetValue(sale);
            itemsList.Clear();
            var result = _validator.TestValidate(sale);
            result.ShouldHaveValidationErrorFor(s => s.Items);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Sale()
        {
            var sale = SaleTestData.GenerateSaleWithItems(2);
            var result = _validator.TestValidate(sale);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
