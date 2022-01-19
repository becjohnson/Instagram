using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserId { get; set; }
        [MaxLength(256), Url]
        public string VanityUrl
        {
            get => UserName;
        }
        [MaxLength(50), Editable(true), ProtectedPersonalData]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [MaxLength(50), Editable(true), ProtectedPersonalData]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [MaxLength(50), Editable(false), ProtectedPersonalData]
        public string FullName
        {
            get => FirstName + LastName;
        }
        [Editable(true)]
        public string ProfilePicture { get; set; }
        public string ProfileSong { get; set; }
        [MaxLength(500), Editable(true)]
        public string Bio { get; set; }
        [Editable(true)]
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public TimeSpan Age
        {
            get => DateTime.Today - Birthday;
        }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
