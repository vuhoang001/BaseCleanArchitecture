using Application.Dtos;
using FluentValidation;

namespace Application.PurchaseRequest.Commands.Create;

public record CreatePurchaseRequestCommand(PurchaseRequestDto Pr) : ICommand<bool>;

public class CreatePurchaseRequestValidator : AbstractValidator<CreatePurchaseRequestCommand>
{
    public CreatePurchaseRequestValidator()
    {
        RuleFor(x => x.Pr.PurchaseRequestItems)
            .NotEmpty().WithMessage("Yêu cầu có ít nhất 1 sản phẩm");

        RuleForEach(x => x.Pr.PurchaseRequestItems)
            .SetValidator(new CreatePurchaseRequestItemValidator());
    }
}

public class CreatePurchaseRequestItemValidator : AbstractValidator<PurchaseRequestItemsDto>
{
    public CreatePurchaseRequestItemValidator()
    {
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Đơn giá phải > 0");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Số lượng phải > 0");
    }
}