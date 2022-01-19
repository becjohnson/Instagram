using Instagram.Data;
using System;
using System.Collections.Generic;

namespace Instagram.Model.Post_Model
{
    public class PostListItem
    {
        //Instagram doesn't show comments, they show content as a comment instead to give the illusion of comments.
        //Show comments in post detail
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
        public string Image { get; set; }
        public string Video { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
