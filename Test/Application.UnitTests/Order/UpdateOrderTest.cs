using Application.Handlers.Order.Commands.Update;
using Application.Interfaces;
using Application.Order.Commands.Update;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Shared.ExceptionBase;
using Xunit.Abstractions;

namespace Application.UnitTests.Order;

public class UpdateOrderTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UpdateOrderTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private async Task<Result<bool>> ExecuteUpdateOrderCommand(string orderName, List<UpdateOrderLineDto> orderItems)
    {
        var mockRepos = CreateMockOrderRepository();
        var dto       = CreateUpdateOrderDto(orderName, orderItems);
        var command   = new UpdateOrderCommand(orderName, dto);
        var handler   = new UpdateOrderHandler(mockRepos.Object);

        return await handler.Handle(command, CancellationToken.None);
    }

    private static UpdateOrderDto CreateUpdateOrderDto(string orderName, List<UpdateOrderLineDto> orderItems)
    {
        return new UpdateOrderDto
        {
            Name       = orderName,
            TotalPrice = 0,
            OrderItems = orderItems
        };
    }

    private Mock<IOrderRepository> CreateMockOrderRepository()
    {
        var mockRepository = new Mock<IOrderRepository>();

        var orders = new Dictionary<string, Domain.Entities.Order>
        {
            {
                "Order 1", new Domain.Entities.Order("P001", "Order 1", new[]
                {
                    ("P001", "Product 1", 20m, 100000m),
                    ("P002", "Product 2", 10m, 250000m)
                })
            },
            {
                "Order 2", new Domain.Entities.Order("P002", "Order 2", new[]
                {
                    ("P003", "Product 3", 30m, 100000m),
                    ("P004", "Product 4", 40m, 250000m)
                })
            },
            {
                "Order 3", new Domain.Entities.Order("P002", "Order 2", new[]
                {
                    ("P005", "Product 5", 30m, 100000m),
                })
            },
        };

        mockRepository.Setup(r => r.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string id) => orders[id]);
        mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Order>())).Returns(Task.FromResult(true));
        return mockRepository;
    }


    [Fact]
    public async Task Update_Order_With_Default()
    {
        var orderItems = new List<UpdateOrderLineDto>
        {
            new()
            {
                ProductCode = "P001",
                ProductName = "Product 1",
                Quantity    = 20,
                UnitPrice   = 100000
            },
            new()
            {
                ProductCode = "P002",
                ProductName = "Product 2",
                Quantity    = 10,
                UnitPrice   = 250000
            }
        };

        var result = await ExecuteUpdateOrderCommand("Order 2", orderItems);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Update_Order_With_Empty_Name()
    {
        var orderItems = new List<UpdateOrderLineDto>
        {
            new()
            {
                ProductCode = "P001",
                ProductName = "Product 1",
                Quantity    = 20,
                UnitPrice   = 100000
            },
            new()
            {
                ProductCode = "P002",
                ProductName = "Product 2",
                Quantity    = 10,
                UnitPrice   = 250000
            }
        };

        var result = await ExecuteUpdateOrderCommand("Order 1", orderItems);


        result.IsSuccess.Should().BeTrue();
        if (!result.IsSuccess)
        {
            _testOutputHelper.WriteLine(result.Errors.ToString());
        }
    }
}