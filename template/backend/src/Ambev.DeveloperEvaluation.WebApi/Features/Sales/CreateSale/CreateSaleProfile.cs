using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleRequest>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemRequest>();
            CreateMap<CreateSaleResponse, CreateSaleResponse>();
            CreateMap<CreateSaleItemResponse, CreateSaleItemResponse>();
        }
    }
}
