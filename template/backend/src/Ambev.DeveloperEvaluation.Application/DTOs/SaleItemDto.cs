using System;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleItemDto
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
