using Instagram.Data;
using System;

namespace Instagram.Model.Post_Model
{
    public class PostEdit
    {
        public string PostId { get; set; }
        public string Content { get; set; }
        public int Brightness { get; set; }
        public int Contrast { get; set; }
        public int Saturation { get; set; }
        public int GreyScale { get; set; }
        public int Sepia { get; set; }
        public int Invert { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
    }
}
