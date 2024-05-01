using AutoMapper;
using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Models;

namespace MotorcycleRental.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<DeliveryMan, DeliveryManViewModel>().ReverseMap();
            CreateMap<Motorcycle, MotorcycleViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Rental, RentalViewModel>().ReverseMap();
        }
    }
}
