using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {

        [Fact]
        public void ConstructorShouldInitializeProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var number = "S123";
            var saleDate = DateTime.Now;
            var customerId = Guid.NewGuid();
            var customerName = "Cliente Teste";
            var branchId = Guid.NewGuid();
            var branchName = "Filial Teste";

            // Act
            var sale = new Ambev.DeveloperEvaluation.Domain.Entities.Sale(id, number, saleDate, customerId, customerName, branchId, branchName);

            // Assert
            Assert.Equal(id, sale.Id);
            Assert.Equal(number, sale.Number);
            Assert.Equal(saleDate, sale.SaleDate);
            Assert.Equal(customerId, sale.CustomerId);
            Assert.Equal(customerName, sale.CustomerName);
            Assert.Equal(branchId, sale.BranchId);
            Assert.Equal(branchName, sale.BranchName);
            Assert.False(sale.Cancelled);
            Assert.Empty(sale.Items);
        }

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
        public void TotalAmountShouldSumAllItemTotals()
        {
            // Arrange
            var sale = SaleTestData.GenerateSaleWithItems(2);
            var expectedTotal = sale.Items.Sum(i => i.Total);

            // Act
            var total = sale.TotalAmount;

            // Assert
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void TotalAmount_ShouldSumAllItemTotals()
        {
            // Arrange
            var sale = new Sale(Guid.NewGuid(), "S123", DateTime.Now, Guid.NewGuid(), "Cliente", Guid.NewGuid(), "Filial");
            var item1 = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Produto 1", 2, 10m, 0m); // 2*10 = 20
            var item2 = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Produto 2", 3, 5m, 0.1m); // 3*5*0.9 = 13.5
            var itemsField = typeof(Sale).GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var itemsList = (List<SaleItem>)itemsField.GetValue(sale);
            itemsList.Add(item1);
            itemsList.Add(item2);

            // Act
            var total = sale.TotalAmount;

            // Assert
            Assert.Equal(33.5m, total);
        }
    }
}
