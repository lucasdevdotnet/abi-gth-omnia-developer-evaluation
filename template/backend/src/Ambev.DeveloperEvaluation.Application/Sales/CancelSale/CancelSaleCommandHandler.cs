using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDomainService _saleDomainService;
        public CancelSaleCommandHandler(ISaleRepository saleRepository,ISaleDomainService saleDomainService)
        {
            _saleRepository = saleRepository;
            _saleDomainService = saleDomainService;
        }
        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Request.Id, cancellationToken);

            if (sale == null) return false;

            _saleDomainService.CancelSale(sale);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            Console.WriteLine($"SaleCancelled: {sale.Id}");
            return true;
        }
    }
}
