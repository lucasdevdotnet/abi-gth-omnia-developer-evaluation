namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid SaleId { get; set; } 

        public Sale? Sale { get; set; }
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total { get; private set; }
        public bool Cancelled { get; private set; }

        public SaleItem(Guid id, Guid productId, string productName, int quantity, decimal unitPrice, decimal discount)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName?.Trim() ?? string.Empty;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            Cancelled = false;
            RecalculateTotal();
        }

        public void RecalculateTotal()
        {
            Total = Quantity * UnitPrice * (1 - Discount);
        }

        public void Cancel()
        {
            Cancelled = true;
            Total = 0m;
        }
    }
}
