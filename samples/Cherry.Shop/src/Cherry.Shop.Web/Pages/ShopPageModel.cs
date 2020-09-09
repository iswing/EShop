using Cherry.Shop.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Cherry.Shop.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class ShopPageModel : AbpPageModel
    {
        protected ShopPageModel()
        {
            LocalizationResourceType = typeof(ShopResource);
        }
    }
}