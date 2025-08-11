using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemCommandHandler : IRequestHandler<CancelSaleItemCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly Domain.Services.ISaleItemDomainService _saleItemDomainService;
        public CancelSaleItemCommandHandler(ISaleRepository saleRepository, Domain.Services.ISaleItemDomainService saleItemDomainService)
        {
            _saleRepository = saleRepository;
            _saleItemDomainService = saleItemDomainService;
        }
        public async Task<bool> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Request.SaleId, cancellationToken);
            if (sale == null) return false;

            var item = sale.Items.FirstOrDefault(i => i.Id == request.Request.ItemId);
            if (item == null) return false;

            _saleItemDomainService.CancelItem(item);

            await _saleRepository.UpdateAsync(sale, cancellationToken);
            return true;
        }
    }
}
