using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Cherry.Shop.Pages
{
    public class Index_Tests : ShopWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
