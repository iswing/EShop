using EasyAbp.EShop;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
           typeof(EShopHttpApiClientModule),
        typeof(PaymentServiceHttpApiClientModule),
        typeof(PaymentServiceWeChatPayHttpApiClientModule),
        typeof(PaymentServicePrepaymentHttpApiClientModule)
    )]
    public class ShopHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ShopApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
