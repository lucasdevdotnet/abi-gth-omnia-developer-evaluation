using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommand : IRequest<GetSaleResponse>
    {
        public GetSaleRequest Request { get; set; }
        public GetSaleCommand(GetSaleRequest request)
        {
            Request = request;
        }
    }
}
