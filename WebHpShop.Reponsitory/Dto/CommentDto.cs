using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class CommentDto 
    {
        public long CommentId { get; set; }
        public string Content { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }

    }
}
