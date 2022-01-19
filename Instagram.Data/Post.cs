using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram.Data
{
    public class Post
    {
        [Key]
        public string PostId { get; set; }
        public Guid OwnerId { get; set; }
        public string Content { get; set; }
        public int Brightness { get; set; }
        public int Contrast { get; set; }
        public int Saturation { get; set; }
        public int GreyScale { get; set; }
        public int Sepia { get; set; }
        public int Invert { get; set; }
        public string Alt { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public ApplicationUser User { get; set; }
        public Tag Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}