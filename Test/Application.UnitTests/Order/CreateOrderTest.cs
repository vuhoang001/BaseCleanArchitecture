using Application.Dtos;
using Application.Handlers.Order.Commands.Create;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using Shared.ExceptionBase;

namespace Application.UnitTests.Order;

public class CreateOrderTest
{
    [Fact]
    public async Task Handle_ValidCommand_Create_Order_And_Return_Id()
    {
        var repo           = new Mock<IOrderRepository>();
        var codeGeneration = new Mock<ICodeGeneration>();

        repo.Setup(r => r.CreateAsync(It.IsAny<Domain.Entities.Order>()))
            .Returns(Task.FromResult(true));

        codeGeneration.Setup(c => c.GenerateCodeAsync<Domain.Entities.Order>(x => x.Code, "ORD-", 5))
            .ReturnsAsync("ORD-00001");

        var dto = new CreateOrderRequest
        {
            Code = "ORD-001",
            Name = "Hoang",
            OrderItems =
            [
                new OrderItemDto { ProductCode = "SKU1", ProductName = "Prod 1", Quantity = 0, UnitPrice = 0 },
                new OrderItemDto { ProductCode = "SKU2", ProductName = "Prod 2", Quantity = 1, UnitPrice = 20 }
            ]
        };

        var cmd     = new CreateOrderCommand(dto);
        var handler = new CreateOrderHandler(repo.Object, codeGeneration.Object);

        var result = await handler.Handle(cmd, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_EmptyCode_Should_Return_Validation_Error()
    {
        // var repo = new Mock<IOrderRepository>();

        var validator = new CreateOrderValidation();

        var dto = new CreateOrderRequest
        {
            Code       = "DH0007",
            Name       = "Hoang",
            TotalPrice = 0,
            OrderItems =
            [
                new OrderItemDto
                {
                    ProductCode = "Hoang",
                    ProductName = "Null",
                    Quantity    = 1,
                    UnitPrice   = 0
                }
            ]
        };

        var cmd    = new CreateOrderCommand(dto);
        var result = await validator.ValidateAsync(cmd, CancellationToken.None);


        result.IsValid.Should().BeFalse();
        // result.Errors.Should().Contain(e => e.ErrorMessage == "Yêu cầu có ít nhất 1 sản phẩm");
        // result.Errors.Should().Contain(e => e.ErrorMessage == "Mã đơn hàng không được để trống");
        result.Errors.Should().Contain(e => e.ErrorMessage == "Đơn giá phải lớn hơn 0");
    }
}