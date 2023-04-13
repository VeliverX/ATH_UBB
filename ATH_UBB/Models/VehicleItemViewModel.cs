using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ATH_UBB.Models
{
    public class VehicleItemViewModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Display(Name = "Cena")]
        public double price { get; set; }

        [Display(Name = "Model")]
        public string model { get; set; }

        [Display(Name = "Znajduje się")]
        public string localization { get; set; }

        [Display(Name = "Dostepność")]
        public bool isAvailable { get; set; }
    }
}
