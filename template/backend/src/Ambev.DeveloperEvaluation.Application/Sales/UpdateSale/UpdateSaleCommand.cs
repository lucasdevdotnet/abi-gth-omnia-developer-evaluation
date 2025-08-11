using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResponse>
    {
        public UpdateSaleRequest Request { get; set; }
        public UpdateSaleCommand(UpdateSaleRequest request)
        {
            Request = request;
        }
    }
}
