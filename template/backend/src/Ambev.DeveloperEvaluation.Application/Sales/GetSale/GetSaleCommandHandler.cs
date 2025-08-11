using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommandHandler : IRequestHandler<GetSaleCommand, GetSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        public GetSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public async Task<GetSaleResponse> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Request.Id, cancellationToken);
            if (sale == null) return null!;
            return new GetSaleResponse
            {
                Id = sale.Id,
                Number = sale.Number,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName,
                TotalAmount = sale.TotalAmount,
                Cancelled = sale.Cancelled,
                Items = sale.Items.Select(i => new GetSaleItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    TotalAmount = i.Total,
                    Cancelled = i.Cancelled
                }).ToList()
            };
        }
    }
}
