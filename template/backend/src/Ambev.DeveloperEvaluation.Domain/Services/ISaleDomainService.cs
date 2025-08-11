using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleDomainService
    {
        void CancelSale(Sale sale);
        void AddItem(Sale sale, System.Guid productId, string? productName, int quantity, decimal unitPrice);
    }
}
