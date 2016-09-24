using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class BasicInfo
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int UserId { get; set; }

        public string ProfilePicUrl { get; set; }

        public string CoverPicUrl { get; set; }

        public string About { get; set; }

        public string AreaOfInterest { get; set; }

        public string Location { get; set; }

        public string Education { get; set; }

        public string Experience { get; set; }


    }
}