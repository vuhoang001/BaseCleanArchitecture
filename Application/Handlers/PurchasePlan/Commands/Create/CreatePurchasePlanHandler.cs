using Application.Handlers.PurchasePlan.Commands.Create;
using Application.Interfaces;
using Shared.ExceptionBase;

namespace Application.PurchasePlan.Commands.Create;

public class CreatePurchasePlanHandler(IPurchasePlanRepos purchasePlanRepos)
    : ICommandHandler<CreatePurchasePlanCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreatePurchasePlanCommand request, CancellationToken cancellationToken)
    {
        var purchasePlanExists = await purchasePlanRepos.FindByCodeAsync(request.Dto.DocCode);
        if (purchasePlanExists is not null)
            return
                Result<bool>.Failure($"Kế hoạch mua hàng với mã {request.Dto.DocCode} đã tồn tại");

        var purchasePlanItems = request.Dto.ItemLines.Select(x => (x.ItemCode, x.ItemName, x.Quantity, x.UnitPrice));
        var purchasePlan =
            new Domain.Entities.PurchasePlan(request.Dto.DocCode, request.Dto.DocName, request.Dto.DocDate,
                purchasePlanItems);

        await purchasePlanRepos.CreateAsync(purchasePlan);
        return Result<bool>.Success(true);
    }
}