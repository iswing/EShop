using EasyAbp.EShop;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(ShopApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(EShopApplicationModule),
        typeof(PaymentServiceApplicationModule),
        typeof(PaymentServiceWeChatPayApplicationModule),
        typeof(PaymentServicePrepaymentApplicationModule)
        )]
    public class ShopApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ShopApplicationModule>();
            });
        }
    }
}
