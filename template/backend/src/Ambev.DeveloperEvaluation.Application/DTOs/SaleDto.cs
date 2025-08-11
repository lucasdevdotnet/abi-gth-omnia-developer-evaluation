using System;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleDto
    {
        public string? Number { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string? BranchName { get; set; }
        public SaleItemDto[]? Items { get; set; }
    }
}
