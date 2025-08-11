using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResponse>
    {
        public CreateSaleRequest Request { get; set; }
        public CreateSaleCommand(CreateSaleRequest request)
        {
            Request = request;
        }
    }
}
