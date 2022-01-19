using Instagram.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Instagram.Model.Post_Model
{
    public class PostCreate
    {
        public string PostId { get; set; }
        public string Content { get; set; }
        public int Brightness { get; set; }
        public int Contrast { get; set; }
        public int Saturation { get; set; }
        public int GreyScale { get; set; }
        public int Sepia { get; set; }
        public int Invert { get; set; }
        public string Alt { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Video { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
