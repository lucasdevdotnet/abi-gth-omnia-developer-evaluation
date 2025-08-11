using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string? Number { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string? BranchName { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Cancelled { get; set; }
        public List<GetSaleItemResponse>? Items { get; set; }
    }
}
