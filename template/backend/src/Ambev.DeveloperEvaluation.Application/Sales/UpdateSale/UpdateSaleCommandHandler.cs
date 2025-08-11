using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IEventPublisherService _eventPublisherService ;
        public UpdateSaleCommandHandler(ISaleRepository saleRepository, IEventPublisherService eventPublisherService)
        {
            _saleRepository = saleRepository;
            _eventPublisherService = eventPublisherService;
        }
        public async Task<UpdateSaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Request.Id, cancellationToken);
            if (sale == null) return null!;
            // Atualiza os campos principais
            // (Ajuste conforme sua entidade permitir setters)
            // sale.Number = request.Request.Number;
            // sale.SaleDate = request.Request.SaleDate;
            // sale.CustomerId = request.Request.CustomerId;
            // sale.CustomerName = request.Request.CustomerName;
            // sale.BranchId = request.Request.BranchId;
            // sale.BranchName = request.Request.BranchName;
            // Atualização dos itens pode exigir lógica adicional
            // Log SaleModified
            _eventPublisherService.Publish(new SaleModifiedEvent(sale.Id));

            await _saleRepository.UpdateAsync(sale, cancellationToken);
            return new UpdateSaleResponse
            {
                Id = sale.Id,
                Number = sale.Number,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new UpdateSaleItemResponse
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
