﻿using Cherry.Shop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Cherry.Shop.Permissions
{
    public class ShopPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ShopPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(ShopPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ShopResource>(name);
        }
    }
}