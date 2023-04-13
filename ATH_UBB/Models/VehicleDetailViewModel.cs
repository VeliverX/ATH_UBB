using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATH_UBB.Models
{
    public class VehicleDetailViewModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Display(Name = "Cena")]
        public double price { get; set; }

        [Display(Name = "Marka")]
        public string brand { get; set; }

        [Display(Name = "Model")]
        public string model { get; set; }

        [Display(Name = "Znajduje się")]
        public string localization { get; set; }

        [Display(Name = "Opis")]
        public string description { get; set; }

        [Display(Name = "Dostepność")]
        public bool isAvailable { get; set; }
    }
}
