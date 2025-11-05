using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Shared;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProduct = Domain.Models.Product;

namespace Services
{
	public class PaymentService(IBasketRepository basketRepository,
		IUnitOfWork unitOfWork,
		IMapper mapper,
		IConfiguration configuration) : IPaymentService
	{
		public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			var basket = await basketRepository.GetBasketByIdAsync(basketId);
			if (basket is null) throw new BasketNotFoundException(basketId);
			foreach(var item in basket.Items)
			{
				var product = await unitOfWork.GetRepository<OrderProduct, int>().GetByIdAsync(item.Id);
				if (product is null) throw new ProductNotFoundException(item.Id);
				item.Price = product.Price;
			}
			if (!basket.DeliveryMethodId.HasValue) throw new Exception("Invalid Delivery Method Id ");
			var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
			if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

			basket.ShippingPrice = deliveryMethod.Cost;
			var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice)*100;
			StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
			var service = new PaymentIntentService();
			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				var createOptions = new PaymentIntentCreateOptions()
				{
					Amount = amount,
					Currency = "USD",
					PaymentMethodTypes = new List<string>() { "card" }
				};
				var paymentIntent = await service.CreateAsync(createOptions);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}
			else
			{
				var updateOptions = new PaymentIntentUpdateOptions()
				{
					Amount = amount,
				};
				await service.UpdateAsync(basket.PaymentIntentId, updateOptions);


			}
			await basketRepository.UpdateBasketAsync(basket);
			var basketDto = mapper.Map<CustomerBasketDto>(basket);
			return basketDto;
		}
	}
}
