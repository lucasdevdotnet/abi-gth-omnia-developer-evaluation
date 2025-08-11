using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemRequest
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
    }
}
