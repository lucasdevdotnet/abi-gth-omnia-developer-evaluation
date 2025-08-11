using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class SaleItemDomainService : ISaleItemDomainService
    {
        private readonly IEventPublisherService _eventPublisher;

        public SaleItemDomainService(IEventPublisherService eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void ValidateItemQuantity(int quantity)
        {
            if (quantity > 20)
                throw new DomainException("It is not possible to sell more than 20 identical items.");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");
        }

        public decimal CalculateItemTotal(int quantity, decimal unitPrice, decimal discount)
        {
            return quantity * unitPrice * (1 - discount);
        }

        public decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10 && quantity <= 20)
                return 0.20m;
            if (quantity >= 4)
                return 0.10m;
            return 0m;
        }

        public SaleItem CreateSaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ValidateItemQuantity(quantity);

            return new SaleItem(Guid.NewGuid(), productId, productName, quantity, unitPrice, CalculateDiscount(quantity));
        }

        public void CancelItem(SaleItem item)
        {
            item.Cancel();

            _eventPublisher.Publish(new ItemCancelledEvent(item.SaleId, item.Id));
        }
    }
}
