using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class CarDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public double PurchasedAmount { get; set; }

        public string RegistrationNumber { get; set; }

        public string Colour { get; set; }

        public double CurrentValue { get; set; }

        public string Status { get; set; }

        public string Notes { get; set; }
    }
}
