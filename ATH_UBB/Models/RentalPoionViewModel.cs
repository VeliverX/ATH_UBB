using System.ComponentModel.DataAnnotations;
namespace ATH_UBB.Models
{
    public class RentalPoionViewModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }
        [Display(Name ="Adres")]
        public string Name { get; set; }
    }
}
