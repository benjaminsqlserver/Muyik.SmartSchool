using Muyik.SmartSchool.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Muyik.SmartSchool.Permissions;

public class SmartSchoolPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SmartSchoolPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(SmartSchoolPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartSchoolResource>(name);
    }
}
