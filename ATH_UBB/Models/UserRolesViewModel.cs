using ATH_UBB.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ATH_UBB.Models
{
    public class UserRolesViewModel
    {
        public ApplicationRole ApplicationRole { get; set; }

        public SelectList Users { get; set; }
    }
}
