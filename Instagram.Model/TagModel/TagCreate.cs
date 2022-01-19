using Instagram.Data;
using System.Collections.Generic;

namespace Instagram.Model.Hashtag_Model
{
    public class TagCreate
    {
        public string TagId { get; set; }
        public string HashTag { get; set; }
        public int PostId { get; set; }
    }
}
