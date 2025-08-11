using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public CancelSaleRequest Request { get; set; }
        public CancelSaleCommand(CancelSaleRequest request)
        {
            Request = request;
        }
    }
}
