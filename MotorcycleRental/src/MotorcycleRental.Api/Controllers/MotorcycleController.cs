using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.Extensions;
using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;

namespace MotorcycleRental.Api.Controllers
{

    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[Controller]")]
    public class MotorcycleController : MainController
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleService _motorcycleService;
        private readonly IMapper _mapper;
        public MotorcycleController(IMotorcycleRepository motorcycleRepository,
                                    IMotorcycleService motorcycleService,
                                    IMapper mapper,
                                    INotifier notifier) : base(notifier)
        {
            _motorcycleRepository = motorcycleRepository;
            _motorcycleService = motorcycleService;
            _mapper = mapper;
        }

        [HttpGet("GetMotorcycles")]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<MotorcycleViewModel>> GetMotorcyclesAsync()
        {
            return _mapper.Map<IEnumerable<MotorcycleViewModel>>(await _motorcycleRepository.GetAll());
        }

        [HttpGet("GetMotorcyclesByPlate")]
        public async Task<ActionResult<Motorcycle>> GetMotorcyclesByPlateAsync(string plate)
        {
            var motorcycle = await _motorcycleService.GetByPlate(plate);

            if (motorcycle == null) return NotFound();

            return motorcycle;
        }

        [HttpPost]
        public async Task<ActionResult<Motorcycle>> AddMotorcycleAsync(MotorcycleViewModel motorcycleViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _motorcycleService.AddMotorcycleAsync(_mapper.Map<Motorcycle>(motorcycleViewModel));

            return CustomResponse(motorcycleViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMotorcycleAsync(string plate, MotorcycleViewModel motorcycleViewModel)
        {
            if (plate != motorcycleViewModel.LicensePlate)
            {
                NotifyError("Plate is divergent");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _motorcycleService.UpdateMotorcycleAsync(_mapper.Map<Motorcycle>(motorcycleViewModel));

            return CustomResponse(motorcycleViewModel);
        }

        [HttpDelete]
        public async Task<ActionResult<MotorcycleViewModel>> DeleteMotorcycleAsync(string plate) 
        {
            var motorcycle = await _motorcycleService.GetByPlate(plate);

            if (motorcycle == null) return NotFound();

            await _motorcycleService.DeleteMotorcycleAsync(motorcycle.Id);

            return CustomResponse();
        }
    }
}
