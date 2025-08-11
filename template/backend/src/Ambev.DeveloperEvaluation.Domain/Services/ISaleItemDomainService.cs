using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleItemDomainService
    {
        SaleItem CreateSaleItem(Guid productId, string productName, int quantity, decimal unitPrice);
        void CancelItem(SaleItem item);

        void ValidateItemQuantity(int quantity);
    }
}
