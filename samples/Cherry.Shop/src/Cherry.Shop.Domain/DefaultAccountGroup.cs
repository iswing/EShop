using EasyAbp.PaymentService.Prepayment.Options.AccountGroups;

namespace Cherry.Shop
{
    public partial class ShopDomainModule
    {
        [AccountGroupName("default")]
        public class DefaultAccountGroup
        {

        }

    }
}
