namespace Domain.Enums;

public class RoleClaims
{
    public const string Admin      = "Admin";
    public const string SuperAdmin = "SuperAdmin";

    public const string PurchaseRequest       = "purchaserequest";
    public const string CreatePurchaseRequest = "purchaseRequest:create";
    public const string UpdatePurchaseRequest = "purchaseRequest:update";
    public const string DeletePurchaseRequest = "purchaseRequest:delete";
    public const string GetPurchaseRequest    = "purchaseRequest:get";
}