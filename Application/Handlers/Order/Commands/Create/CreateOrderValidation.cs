using FluentValidation;

namespace Application.Handlers.Order.Commands.Create;

public class CreateOrderValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidation()
    {
        RuleFor(x => x.Order.OrderItems)
            .NotEmpty()
            .WithMessage("Yêu cầu có ít nhất 1 sản phẩm");

        RuleFor(x => x.Order.Name)
            .NotEmpty()
            .WithMessage("Yêu cầu nhập tên cho đơn hàng");

        RuleForEach(x => x.Order.OrderItems).ChildRules(item =>
        {
            item.RuleFor(x => x.ProductCode).NotEmpty();
            item.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Số lượng sản phẩm phải lớn hơn 0");
            item.RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Đơn giá phải lớn hơn 0");
        });
    }
}