using Microsoft.AspNetCore.Identity;

namespace ATH_UBB.Areas.Admin.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public int userCount { get; set; }  
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
    }
}
