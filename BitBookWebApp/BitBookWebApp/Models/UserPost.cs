using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class UserPost
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string  PostText { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CurrretTime { get; set; }
    }
}