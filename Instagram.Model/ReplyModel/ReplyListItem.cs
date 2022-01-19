using Instagram.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Reply_Model
{
    public class ReplyListItem
    {
        public string ReplyId { get; set; }
        [MaxLength(500, ErrorMessage = "Your reply has too many characters."), Required, Editable(true)]
        public string Content { get; set; }
        [Display(Name = "Created at:"), Required, Editable(false)]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified at:"), Editable(true)]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public ApplicationUser User { get; set; }
    }
}
