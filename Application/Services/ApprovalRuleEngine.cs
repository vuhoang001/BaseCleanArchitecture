using Application.Interfaces;
using Application.Interfaces.Approval;
using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Shared.ExceptionBase;

namespace Application.Services;

public class ApprovalRuleEngine : IApprovalRuleEngine
{
    private readonly IServiceProvider                                                           _serviceProvider;
    private          Dictionary<string, Func<IApprovableEntity, CancellationToken, Task<bool>>> _processors = new();

    public ApprovalRuleEngine(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        RegisterProcessor();
    }

    public async Task<bool> BuildApprovalPlanAsync(IApprovableEntity entity, CancellationToken ct)
    {
        if (!_processors.TryGetValue(entity.EntityType, out var processor))
        {
            throw new NotSupportedException($"No rule processor found for entity type: {entity.EntityType}");
        }

        var isSuccessed = await processor(entity, ct);
        return isSuccessed;
    }

    public bool IsSupported(string entityType)
    {
        return _processors.ContainsKey(entityType);
    }

    public IEnumerable<string> GetSupportedEntityTypes()
    {
        return _processors.Keys;
    }

    private void RegisterProcessor()
    {
        _processors =
            new Dictionary<string, Func<IApprovableEntity, CancellationToken, Task<bool>>>(StringComparer
                .OrdinalIgnoreCase)
            {
                { "Order", ProcessOrderApprovalAsync },
            };
    }


    #region Order Processing

    private async Task<bool> ProcessOrderApprovalAsync(IApprovableEntity entity, CancellationToken ct)
    {
        var order = entity as Domain.Entities.Order;
        if (order is null) throw new ApiBadRequestException("Invalid entity type for Order approval processing");

        var orderRepo = _serviceProvider.GetRequiredService<IOrderRepository>();

        var orderExist = await orderRepo.FindByIdAsync(order.Id);
        if (orderExist is null) return false;
        return true;
    }

    #endregion
}