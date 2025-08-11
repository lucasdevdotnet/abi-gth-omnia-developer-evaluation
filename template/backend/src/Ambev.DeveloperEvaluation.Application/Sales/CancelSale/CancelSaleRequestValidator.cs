using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
    {
        public CancelSaleRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
