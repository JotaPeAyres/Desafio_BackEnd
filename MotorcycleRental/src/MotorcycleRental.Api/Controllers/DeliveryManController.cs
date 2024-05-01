using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.Extensions;
using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;

namespace MotorcycleRental.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class DeliveryManController : MainController
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IDeliveryManService _deliveryManService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public DeliveryManController(IDeliveryManRepository deliveryManRepository,
                                        IMapper mapper,
                                        IDeliveryManService deliveryManService, 
                                        INotifier notifier,
                                        UserManager<IdentityUser> userManager) : base(notifier) 
        {
            _deliveryManRepository = deliveryManRepository;
            _mapper = mapper;
            _deliveryManService = deliveryManService;
            _userManager = userManager;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<DeliveryManViewModel>> GetDeliveryMenAsync()
        {
            return _mapper.Map<IEnumerable<DeliveryManViewModel>>(await _deliveryManRepository.GetAll());
        }

        [HttpGet("get-notified")]
        public async Task<ActionResult<IEnumerable<DeliveryManViewModel>>> GetNotifiedDeliveryMan(Guid orderId)
        {
            if (orderId == null)
            {
                NotifyError("It's necessary to pass the orderId");
                return CustomResponse();
            }
            var deliveryMen = await _deliveryManService.GetNotifiedDeliveryMan(orderId);

            if (deliveryMen == null)
                return NotFound();

            return CustomResponse(deliveryMen);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryManViewModel>> RegisterDeliveryManAsync(DeliveryManViewModel deliveryManViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse();

            var userId = _userManager.GetUserAsync(User).Result.Id;

            await _deliveryManService.AddDeliveryManAsync(_mapper.Map<DeliveryMan>(deliveryManViewModel), Guid.Parse(userId));

            return CustomResponse(deliveryManViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDriverLicense(IFormFile file)
        {
            if(ModelState.IsValid) return CustomResponse(ModelState);

            var userId = _userManager.GetUserAsync(User).Result.Id;

            if (file == null || file.Length <= 0)
            {
                NotifyError("Provide an image for registration");
                return CustomResponse();
            }

            var fileExtension = file.FileName.Split(".")[1];
            if (fileExtension != "png" && fileExtension != "bmp")
            {
                NotifyError("File extension not accept");
                return CustomResponse();
            }

            var imageName = Guid.NewGuid() + "_" + file.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);

            if (System.IO.File.Exists(path))
            {
                NotifyError("There is already an image with this name");
                return CustomResponse();
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _deliveryManService.UpdateDriverLicence(imageName, Guid.Parse(userId));

            return CustomResponse();
        }
    }
}
