using System.ComponentModel.DataAnnotations;

namespace Instagram.Data
{
    public class Profile : ApplicationUser
    {
        [Key]
        public string ProfileId { get; set; }
    }
}
