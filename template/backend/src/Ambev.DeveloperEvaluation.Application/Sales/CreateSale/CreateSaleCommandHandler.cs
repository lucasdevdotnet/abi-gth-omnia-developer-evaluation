using Ambev.DeveloperEvaluation.Domain.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDomainService _saleDomainService;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, ISaleDomainService saleDomainService)
        {
            _saleRepository = saleRepository;
            _saleDomainService = saleDomainService;
        }

        public async Task<CreateSaleResponse> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale(
                Guid.NewGuid(),
                request.Request.Number ?? string.Empty,
                request.Request.SaleDate,
                request.Request.CustomerId,
                request.Request.CustomerName ?? string.Empty,
                request.Request.BranchId,
                request.Request.BranchName ?? string.Empty
            );

            if (request.Request.Items != null)
            {
                foreach (var item in request.Request.Items)
                {
                    _saleDomainService.AddItem(sale, item.ProductId,item.ProductName, item.Quantity, item.UnitPrice);
                }
            }

            await _saleRepository.AddAsync(sale, cancellationToken);

            return new CreateSaleResponse
            {
                Id = sale.Id,
                Number = sale.Number,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new CreateSaleItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    TotalAmount = i.Total
                }).ToList()
            };
        }
    }
}
