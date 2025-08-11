using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleItemProfile : Profile
    {
        public CreateSaleItemProfile()
        {
            CreateMap<Application.Sales.CreateSale.CreateSaleItemResponse, CreateSaleItemResponse>();
        }
    }
}
