using EasyAbp.EShop;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.Prepayment.Options;
using EasyAbp.PaymentService.Prepayment.PaymentService;
using EasyAbp.PaymentService.WeChatPay;

using Cherry.Shop.MultiTenancy;
using Cherry.Shop.ObjectExtending;
using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
                typeof(EShopDomainModule),
        typeof(PaymentServiceDomainModule),
        typeof(PaymentServiceWeChatPayDomainModule),
        typeof(PaymentServicePrepaymentDomainModule)
        )]
    public partial class ShopDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ShopDomainObjectExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
            ConfigurePaymentServicePrepayment();

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var resolver = context.ServiceProvider.GetService<IPaymentServiceResolver>();

            resolver.TryRegisterProvider(FreePaymentServiceProvider.PaymentMethod, typeof(FreePaymentServiceProvider));
            resolver.TryRegisterProvider(WeChatPayPaymentServiceProvider.PaymentMethod, typeof(WeChatPayPaymentServiceProvider));
            resolver.TryRegisterProvider(PrepaymentPaymentServiceProvider.PaymentMethod, typeof(PrepaymentPaymentServiceProvider));
        }

        private void ConfigurePaymentServicePrepayment()
        {
            Configure<PaymentServicePrepaymentOptions>(options =>
            {
                options.AccountGroups.Configure<DefaultAccountGroup>(accountGroup =>
                {
                    accountGroup.Currency = "CNY";
                });
            });
        }

    }
}
