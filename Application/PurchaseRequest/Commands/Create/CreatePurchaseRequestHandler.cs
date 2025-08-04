using Application.Data;

namespace Application.PurchaseRequest.Commands.Create;

public class CreatePurchaseRequestHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreatePurchaseRequestCommand, bool>
{
    public async Task<bool> Handle(CreatePurchaseRequestCommand request, CancellationToken cancellationToken)
    {
        var pr = new Domain.Entities.PurchaseRequest(request.Pr.Code, request.Pr.Name);
        foreach (var x in request.Pr.PurchaseRequestItems)
        {
            pr.AddItem(x.ProductCode, x.ProductName, x.Quantity, x.UnitPrice);
        }

        dbContext.PurchaseRequest.Add(pr);
        await dbContext.SaveChangesAsync();
        return true;
    }
}