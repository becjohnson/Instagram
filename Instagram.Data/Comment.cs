using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram.Data
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; }
        public Guid OwnerId { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
        [Display(Name = "Created at:"), Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified at:"), Editable(true)]
        public DateTimeOffset? ModifiedUtc { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}