using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemCommand : IRequest<bool>
    {
        public CancelSaleItemRequest Request { get; set; }
        public CancelSaleItemCommand(CancelSaleItemRequest request)
        {
            Request = request;
        }
    }
}
