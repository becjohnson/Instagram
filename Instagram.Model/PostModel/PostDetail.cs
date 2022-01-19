using Instagram.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Instagram.Model.Post_Model
{
    public class PostDetail
    {
        private readonly Post post;
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
        public IFormFile Image { get; set; }
        public string ImageLocation
        {
            get => post.Image;
            set { }
        }
        public IFormFile Video { get; set; }
        public string VideoLocation
        {
            get => post.Video;
            set { }
        }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string ProfilePictureLocation { get => post.ProfilePicture; set { } }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Tag Tags { get; set; }
    }
}
