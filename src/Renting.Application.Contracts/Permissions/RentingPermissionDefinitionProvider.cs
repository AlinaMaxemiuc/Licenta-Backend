using Renting.Localization;

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Renting.Permissions;

public class RentingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var droneGroup = context.AddGroup(RentingPermissions.GroupName, L("Permission:Renting"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(RentingPermissions.MyPermission1, L("Permission:MyPermission1"));
        var customersPermission = droneGroup.AddPermission(RentingPermissions.Customers.Default, L("Permission:Customers"));
        customersPermission.AddChild(RentingPermissions.Customers.Create, L("Permission:Customers.Create"));
        customersPermission.AddChild(RentingPermissions.Customers.Edit, L("Permission:Customers.Edit"));
        customersPermission.AddChild(RentingPermissions.Customers.Delete, L("Permission:Customers.Delete"));

        var dronesPermission = droneGroup.AddPermission(RentingPermissions.Drones.Default, L("Permission:Drones"));
        dronesPermission.AddChild(RentingPermissions.Drones.Create, L("Permission:Drones.Create"));
        dronesPermission.AddChild(RentingPermissions.Drones.Edit, L("Permission:Drones.Edit"));
        dronesPermission.AddChild(RentingPermissions.Drones.Delete, L("Permission:Drones.Delete"));

        var reviewsPermission = droneGroup.AddPermission(RentingPermissions.Reviews.Default, L("Permission:Reviews"));
        reviewsPermission.AddChild(RentingPermissions.Reviews.Create, L("Permission:Reviews.Create"));
        reviewsPermission.AddChild(RentingPermissions.Reviews.Edit, L("Permission:Reviews.Edit"));
        reviewsPermission.AddChild(RentingPermissions.Reviews.Delete, L("Permission:Reviews.Delete"));

    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RentingResource>(name);
    }
}
