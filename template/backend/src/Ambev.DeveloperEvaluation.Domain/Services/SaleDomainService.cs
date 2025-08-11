using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class SaleDomainService : ISaleDomainService
    {
        private readonly IEventPublisherService _eventPublisher;
        private readonly ISaleItemDomainService _saleItemDomainService;
        public SaleDomainService(IEventPublisherService eventPublisher, ISaleItemDomainService saleItemDomainService)
        {
            _eventPublisher = eventPublisher;
            _saleItemDomainService = saleItemDomainService;
        }

        public void AddItem(Sale sale, Guid productId, string? productName, int quantity, decimal unitPrice)
        {
            if (sale.Cancelled)
                throw new DomainException("Cannot add item to a cancelled sale.");

            var item = _saleItemDomainService.CreateSaleItem(productId, productName, quantity, unitPrice);

            var itemsField = typeof(Sale).GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (itemsField == null)
                throw new InvalidOperationException("Items field not found in Sale entity.");

            var itemsList = (List<SaleItem>)itemsField.GetValue(sale);
            if (itemsList == null)
                throw new InvalidOperationException("Items list is null in Sale entity.");

            itemsList.Add(item);

            _eventPublisher.Publish(new SaleCreatedEvent(sale.Id));
        }

        public void CancelSale(Sale sale)
        {
            if (sale.Cancelled)
                return;
            sale.Cancelled = true;
            foreach (var item in sale.Items)
                item.Cancel();
            _eventPublisher.Publish(new SaleCancelledEvent(sale.Id));
        }
    }
}
