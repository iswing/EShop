using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.WeChatPay;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;


namespace Cherry.Shop
{
    [Dependency(ServiceLifetime.Singleton, ReplaceServices = true, TryRegister = true)]
    public class CherryClaimPaymentOpenIdProvider : IPaymentOpenIdProvider
    {
        public const string OpenIdClaimType = "WeChatOpenId";

        private readonly ICurrentUser _currentUser;

        public CherryClaimPaymentOpenIdProvider(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public Task<string> FindUserOpenIdAsync(string appId, Guid userId)
        {
            return Task.FromResult(userId == _currentUser.Id
                ? _currentUser.FindClaim(OpenIdClaimType)?.Value
                : null);
        }
    }

    public class CherryOrderGenerator : NewOrderGenerator
    {
        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;

        public override async Task<Order> GenerateAsync(CreateOrderDto input, Dictionary<Guid, ProductDto> productDict)
        {
            var order = await base.GenerateAsync(input, productDict);

            var paymentItems = order.OrderLines.Select(itemEto =>
                {
                    var item = new PaymentItem(_guidGenerator.Create(), itemEto.ProductUniqueName,
                        itemEto.Id.ToString(),
                        itemEto.TotalPrice);
                    return item;
                }
            ).ToList();

            if (await HasDuplicatePaymentItemInProgressAsync(paymentItems))
            {
                throw new DuplicatePaymentRequestException();
            }


            var payment = new Payment(_guidGenerator.Create(), _currentTenant.Id,
                _currentUser.GetId(),
                WeChatPayPaymentServiceProvider.PaymentMethod, order.Currency,
                order.OrderLines.Select(item => item.TotalPrice).Sum(),
                paymentItems);

            await _paymentRepository.InsertAsync(payment, autoSave: true);

            //微信统一下单
            await _paymentManager.StartPaymentAsync(payment,
                new[]
                {
                    new {key = "appid", value = "wxb66cbd34dd301219"},
                    new {key = "attach", value = "音桃引擎"},
                    // new {key = "detail", value = "深圳分店"},
                    new {key = "trade_type", value = "NATIVE"},
                    new {key = "body", value = order.OrderLines.Select(x => x.SkuName).JoinAsString("")},
                }.ToDictionary(x => x.key, x => x.value as object));

            payment.MapExtraPropertiesTo(order, MappingPropertyDefinitionChecks.None);

            return order;
        }

        protected virtual async Task<bool> HasDuplicatePaymentItemInProgressAsync(IEnumerable<PaymentItem> paymentItems)
        {
            foreach (var item in paymentItems)
            {
                if (await _paymentRepository.FindPaymentInProgressByPaymentItem(item.ItemType, item.ItemKey) != null)
                {
                    return true;
                }
            }

            return false;
        }

        public CherryOrderGenerator(
            IPaymentManager paymentManager,
            IPaymentRepository paymentRepository,
            IGuidGenerator guidGenerator, ICurrentTenant currentTenant, ICurrentUser currentUser,
            IOrderNumberGenerator orderNumberGenerator, IProductSkuDescriptionProvider productSkuDescriptionProvider) :
            base(guidGenerator, currentTenant, currentUser, orderNumberGenerator, productSkuDescriptionProvider)
        {
            _paymentManager = paymentManager;
            _paymentRepository = paymentRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
        }
    }
}