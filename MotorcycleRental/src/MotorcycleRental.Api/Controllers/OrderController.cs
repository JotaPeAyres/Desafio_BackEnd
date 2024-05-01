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
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : MainController
    {
        private readonly IOrderService _orderService ;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(INotifier notifier,
                                IOrderService orderService,
                                UserManager<IdentityUser> userManager,
                                IMapper mapper) : base(notifier) 
        {
            _orderService = orderService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrderAsync(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse();

            await _orderService.AddOrderAsync(_mapper.Map<Order>(orderViewModel));

            return CustomResponse();
        }

        [Authorize(Roles = "Driver")]
        [HttpPut("take-order")]
        public async Task<ActionResult> TakeOrderAsync(Guid orderId)
        {
            if (orderId == null)
            {
                NotifyError("It's necessary to pass the orderId");
                return CustomResponse();
            }

            var userId = _userManager.GetUserAsync(User).Result.Id;

            _orderService.TakeOrderAsync(orderId, Guid.Parse(userId));

            return CustomResponse();
        }

        [Authorize(Roles = "Driver")]
        [HttpPut("finalize-order")]
        public async Task<ActionResult> FinalizeOrderAsync(Guid orderId)
        {
            if (orderId == null)
            {
                NotifyError("It's necessary to pass the orderId");
                return CustomResponse();
            }

            var userId = _userManager.GetUserAsync(User).Result.Id;

            _orderService.FinalizeOrderAsync(orderId, Guid.Parse(userId));

            return CustomResponse();
        }
    }
}
