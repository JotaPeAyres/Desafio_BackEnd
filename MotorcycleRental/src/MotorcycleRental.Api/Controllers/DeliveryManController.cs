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
        /////////////// precisa arrumar
        public async Task<ActionResult<IEnumerable<DeliveryManViewModel>>> GetNotifiedDeliveryMan()
        {
            var deliveryMen = _mapper.Map<IEnumerable<DeliveryManViewModel>>(await _deliveryManRepository.GetAll());

            if (deliveryMen == null)
                return NotFound();

            return Ok(deliveryMen);
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
        public async Task<ActionResult> UpdateDriverLicense(string image, Guid deliveryManId)
        {
            if(ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "_" + image;
            //if(await !UploadImage(image, imageName))
            //{
            //    return CustomResponse();
            //}
            return CustomResponse();
        }

        //private async Task <bool> UploadImage(IFormFile file, string imgPrefix)
        //{
        //    if (file == null || file.Length <= 0)
        //    {
        //        NotifyError("Provide an image for registration");
        //        return false;
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

        //    if (System.IO.File.Exists(path))
        //    {
        //        ModelState.AddModelError(string.Empty, "There is already an image with this name");
        //        return false;
        //    }

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return true;
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddImage(IFormFile file)
        //{
        //    return Ok(file);    
        //}
    }
}
