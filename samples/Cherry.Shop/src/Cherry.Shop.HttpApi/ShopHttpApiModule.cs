using EasyAbp.EShop;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
                typeof(EShopHttpApiModule),
        typeof(PaymentServiceHttpApiModule),
        typeof(PaymentServiceWeChatPayHttpApiModule),
        typeof(PaymentServicePrepaymentHttpApiModule)
        )]
    public class ShopHttpApiModule : AbpModule
    {
        
    }
}
