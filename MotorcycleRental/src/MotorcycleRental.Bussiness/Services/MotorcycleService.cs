using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using MotorcycleRental.Bussiness.Models.Validations;

namespace MotorcycleRental.Bussiness.Services
{
    public class MotorcycleService : BaseService, IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        public MotorcycleService(IMotorcycleRepository motorcycleRepository, INotifier notifier) : base(notifier) 
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task AddMotorcycleAsync(Motorcycle motorcycle)
        {
            if (!ExecuteValidation(new MotorcycleValidation(), motorcycle)) return;

            if(_motorcycleRepository.GetBy(g => g.LicensePlate == motorcycle.LicensePlate).Result.Any())
            {
                Notify("There is already a motorcycle with this plate");
                return;
            }

            await _motorcycleRepository.Add(motorcycle);
        }

        public async Task UpdateMotorcycleAsync(Motorcycle motorcycle)
        {
            if (!ExecuteValidation(new MotorcycleValidation(), motorcycle)) return;

            if (_motorcycleRepository.GetBy(g => g.LicensePlate == motorcycle.LicensePlate).Result.Any())
            {
                Notify("There is already a motorcycle with this plate");
                return;
            }

            await _motorcycleRepository.Update(motorcycle);
        }

        public async Task DeleteMotorcycleAsync(Guid Id)
        {
            if(await _motorcycleRepository.IsRented(Id))
            {
                Notify("This motorcycle is on a rental");
                return;
            }

            await _motorcycleRepository.Delete(Id);
        }
    }
}
