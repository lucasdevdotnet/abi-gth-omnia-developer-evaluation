using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
    {
        public CancelSaleItemRequestValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty();
            RuleFor(x => x.ItemId).NotEmpty();
        }
    }
}
