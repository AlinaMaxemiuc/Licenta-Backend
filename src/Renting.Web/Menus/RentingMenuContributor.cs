using System.Threading.Tasks;

using Renting.Localization;
using Renting.MultiTenancy;
using Renting.Permissions;

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Renting.Web.Menus;

public class RentingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<RentingResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                RentingMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
        new ApplicationMenuItem(
           "Renting",
           l["Menu:Renting"],
           icon: "fa fa-dot-circle-o"
       ).AddItem(
             new ApplicationMenuItem(
                "Renting.Customers",
                l["Menu:Customers"],
                url: "/Customers"
             ).RequirePermissions(RentingPermissions.Customers.Default)
        ).AddItem(
            new ApplicationMenuItem(
                "Renting.Drones",
                l["Menu:Drones"],
                url: "/Drones"
            ).RequirePermissions(RentingPermissions.Drones.Default)
        ).AddItem(
            new ApplicationMenuItem(
                "Renting.Reviews",
                l["Menu:Reviews"],
                url: "/Reviews"
            ).RequirePermissions(RentingPermissions.Reviews.Default)
        ));
        return Task.CompletedTask;
    }
}
