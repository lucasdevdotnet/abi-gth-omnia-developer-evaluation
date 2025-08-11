using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sale
{
    public class SaleTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            // Act
            var sale = SaleTestData.GenerateValidSale();

            // Assert
            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.False(string.IsNullOrWhiteSpace(sale.Number));
            Assert.NotEqual(default(DateTime), sale.SaleDate);
            Assert.NotEqual(Guid.Empty, sale.CustomerId);
            Assert.False(string.IsNullOrWhiteSpace(sale.CustomerName));
            Assert.NotEqual(Guid.Empty, sale.BranchId);
            Assert.False(string.IsNullOrWhiteSpace(sale.BranchName));
            Assert.False(sale.Cancelled);
            Assert.Empty(sale.Items);
        }

        [Fact]
        public void TotalAmount_ShouldSumAllItemTotals()
        {
            // Arrange
            var sale = SaleTestData.GenerateSaleWithItems(2);
            var expectedTotal = sale.Items.Sum(i => i.Total);

            // Act
            var total = sale.TotalAmount;

            // Assert
            Assert.Equal(expectedTotal, total);
        }
    }
}
