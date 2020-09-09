using Volo.Abp.Modularity;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopApplicationModule),
        typeof(ShopDomainTestModule)
        )]
    public class ShopApplicationTestModule : AbpModule
    {

    }
}