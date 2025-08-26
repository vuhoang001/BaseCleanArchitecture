namespace Shared.Extensions;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class PermissionAttribute : Attribute
{
    public string[] RoleAndPermissions { get; }

    public PermissionAttribute(params string[] roleAndPermissions)
    {
        RoleAndPermissions = roleAndPermissions;
    }
}