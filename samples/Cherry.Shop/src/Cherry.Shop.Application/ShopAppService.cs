using System;
using System.Collections.Generic;
using System.Text;
using Cherry.Shop.Localization;
using Volo.Abp.Application.Services;

namespace Cherry.Shop
{
    /* Inherit your application services from this class.
     */
    public abstract class ShopAppService : ApplicationService
    {
        protected ShopAppService()
        {
            LocalizationResource = typeof(ShopResource);
        }
    }
}
