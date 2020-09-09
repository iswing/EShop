using System.Threading.Tasks;

namespace Cherry.Shop.Data
{
    public interface IShopDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
