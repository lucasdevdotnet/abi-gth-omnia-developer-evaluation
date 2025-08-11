using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleRequest, Application.Sales.GetSale.GetSaleRequest>();
            CreateMap<Application.Sales.GetSale.GetSaleResponse, GetSaleResponse>();
            CreateMap<Application.Sales.GetSale.GetSaleItemResponse, GetSaleItemResponse>();
        }
    }
}
