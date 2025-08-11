using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get;  set; }
        public DateTime SaleDate { get;  set; }
        public Guid CustomerId { get;  set; }
        public Guid BranchId { get;  set; }
        public string Number { get;  set; }
        public string CustomerName { get;  set; }
        public string BranchName { get;  set; }
        public bool Cancelled { get;  set; }

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        public decimal TotalAmount => _items.Sum(i => i.Total);

        private readonly ISaleDomainService? _saleDomainService;

        // Construtor para o EF Core
        public Sale() { }


        // Construtor de domínio
        public Sale(Guid id, string number, DateTime saleDate, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            Id = id;
            Number = number?.Trim() ?? string.Empty;
            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName?.Trim() ?? string.Empty;
            BranchId = branchId;
            BranchName = branchName?.Trim() ?? string.Empty;
            Cancelled = false;
        }
    }
}
