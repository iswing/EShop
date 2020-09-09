using EasyAbp.EShop;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(EShopApplicationContractsModule),
        typeof(PaymentServiceApplicationContractsModule),
        typeof(PaymentServiceWeChatPayApplicationContractsModule),
        typeof(PaymentServicePrepaymentApplicationContractsModule)
    )]
    public class ShopApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ShopDtoExtensions.Configure();
        }
    }
}
