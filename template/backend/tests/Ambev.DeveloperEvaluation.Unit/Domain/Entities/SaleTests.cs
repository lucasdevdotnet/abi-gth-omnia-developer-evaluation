using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;
namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {

        [Fact]
        public void Constructor_ShouldInitializeProperties()
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
            var sale = new Sale(id, number, saleDate, customerId, customerName, branchId, branchName);

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
