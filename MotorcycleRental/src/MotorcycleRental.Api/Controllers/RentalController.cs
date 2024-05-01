using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using System.Runtime.InteropServices;

namespace MotorcycleRental.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : MainController
    {
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;
        public RentalController(IRentalService rentalService,
                                IMapper mapper,
                                INotifier notifier) : base (notifier)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }


        [Authorize(Roles = "Driver")]
        [HttpPost]
        public async Task<ActionResult> AddRentalAsync(RentalViewModel rentalViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _rentalService.AddRentalAsync(_mapper.Map<Rental>(rentalViewModel));

            return CustomResponse();
        }


        [Authorize(Roles = "Driver")]
        [HttpPut]
        public async Task<ActionResult<string>> FinishRentalAsync(Guid rentalId, DateTime endDate)
        {
            if(rentalId == null || endDate == null)
            {
                NotifyError("Fill in all the information");
            }

            var finalCust = await _rentalService.FinishRentalAsync(rentalId, endDate);
            if(finalCust == 0)
            {
                return CustomResponse();
            }

            return $"The final cost of the rental was R${finalCust}";
        }
    }
}
