using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class OrderService(IMapper mapper,
		IBasketRepository basketRepository,
		IUnitOfWork unitOfWork) : IOrderService
	{
		public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequestDto, string userEmail)
		{
			var address = mapper.Map<Address>(orderRequestDto.ShipToAddress);

			var basket = await basketRepository.GetBasketByIdAsync(orderRequestDto.BasketId);
			if (basket is null) throw new BasketNotFoundException(orderRequestDto.BasketId);

			var orderItems =new List<OrderItem>();
			foreach (var item in basket.Items) 
			{
				var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
				if (product is null) throw new ProductNotFoundException(item.Id);
				var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
				var orderItem = new OrderItem(productInOrderItem,item.Quantity,product.Price);
				orderItems.Add(orderItem);
			}
			var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequestDto.DeliveryMethodId);
			if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(orderRequestDto.DeliveryMethodId);

			var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
			var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);
			var existOrder = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
			if (existOrder is not null)
				unitOfWork.GetRepository<Order, Guid>().Delete(existOrder);
			var order = new Order(userEmail, address, orderItems, deliveryMethod, subTotal, " ");
			await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
			var count = await unitOfWork.SaveChangesAsync();
			if (count == 0) throw new OrderCreateBadRequestException();
			var orderDto = mapper.Map<OrderResultDto>(order);
			return orderDto;
			
		}

		public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
		{
			var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
			var deliveryMethodsDto = mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
			return deliveryMethodsDto;
		}

		public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
		{
			var spec = new OrderSpecification(id);
			var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
			if (order is null) throw new OrderNotFoundException(id);
			var orderDto = mapper.Map<OrderResultDto>(order);
			return orderDto;
		}

		public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail)
		{
			var spec = new OrderSpecification(userEmail);
			var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
			var ordersDto = mapper.Map<IEnumerable<OrderResultDto>>(orders);
			return ordersDto;

		}
	}
}
