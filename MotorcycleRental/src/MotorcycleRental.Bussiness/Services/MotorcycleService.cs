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

            var oldMotorcycle = _motorcycleRepository.GetByFirst(g => g.LicensePlate == motorcycle.LicensePlate).Result;

            if(oldMotorcycle == null)
            {
                Notify("Motorcycle not found");
                return;
            }

            if (_motorcycleRepository.GetBy(g => g.LicensePlate == motorcycle.LicensePlate && g.Id != oldMotorcycle.Id).Result.Any())
            {
                Notify("There is already a motorcycle with this plate");
                return;
            }

            oldMotorcycle.LicensePlate = motorcycle.LicensePlate;
            oldMotorcycle.Year = motorcycle.Year;
            oldMotorcycle.Model = motorcycle.Model;

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

        public async Task<IEnumerable<Motorcycle>> GetAll()
        {
            return await _motorcycleRepository.GetAll();
        }

        public async Task<Motorcycle> GetByPlate(string plate)
        {
            if (string.IsNullOrEmpty(plate))
            {
                Notify("It's necessary to pass the plate");
                return null;
            }

            return await _motorcycleRepository.GetByFirst(g => g.LicensePlate == plate);
        }
    }
}
