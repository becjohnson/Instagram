using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram.Data
{
    public class Tag
    {
        [Key]
        public string TagId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}