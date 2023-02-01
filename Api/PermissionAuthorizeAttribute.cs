using Microsoft.AspNetCore.Authorization;

namespace Api;

public class CustomClaimTypes
{
    public const string Permission = "Permission";
}

public static class UserPermission
{
    
    
    
    public const string AddressRead = "Address.Read";
    public const string AddressCreate = "Address.Create";
    public const string AddressUpdate = "Address.Update";
    public const string AddressDelete = "Address.Delete";
}