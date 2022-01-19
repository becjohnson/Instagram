using Instagram.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Comment_Model
{
    public class CommentCreate
    {
        public string CommentId { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
        [Display(Name = "Created at:"), Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified at:"), Editable(true)]
        public DateTimeOffset? ModifiedUtc { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public ApplicationUser User { get; set; }
    }
}
