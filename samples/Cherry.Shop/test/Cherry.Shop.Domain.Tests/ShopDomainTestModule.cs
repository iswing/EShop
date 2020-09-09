using Cherry.Shop.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Cherry.Shop
{
    [DependsOn(
        typeof(ShopEntityFrameworkCoreTestModule)
        )]
    public class ShopDomainTestModule : AbpModule
    {

    }
}