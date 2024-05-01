using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MotorcycleRental.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : MainController
    {
        private readonly IOrderService _orderService ;
        private readonly IMapper _mapper;
        public OrderController(INotifier notifier,
                                IOrderService orderService,
                                IMapper mapper) : base(notifier) 
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderAsync(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse();

            await _orderService.AddOrderAsync(_mapper.Map<Order>(orderViewModel));

            //{ 
            //    string message = JsonSerializer.Serialize(orderViewModel);
            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(exchange: "",
            //        routingKey: "orderQueue",
            //        basicProperties: null,
            //        body: body);
            //}
            return CustomResponse();
        }
    }
}
