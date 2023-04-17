using System.ComponentModel.DataAnnotations;
namespace ATH_UBB.Models
{
    public class RentalPointViewModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }


        [Display(Name ="Adres")]
        public string Adres { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

    }
}
