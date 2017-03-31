using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class LikePost
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CurrretTime { get; set; }
    }
}