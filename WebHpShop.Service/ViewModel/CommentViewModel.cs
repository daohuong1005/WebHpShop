using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class CommentViewModel
    {
        public long CommentId { get; set; }
        public string Content { get; set; }
        public long ProductId { get; set; }
        public DateTime DateCreate { get; set; }
        public string NameUser { get; set; }

    }
}
