using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Api.ViewModels
{
    public class MotorcycleViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Year { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Model { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string LicensePlate { get; set; }
    }
}
