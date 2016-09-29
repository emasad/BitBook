using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class Settings
    {

    }

    public class ChangePass : Settings
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter your current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter your new password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm your new password here")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeCoverPhoto : Settings
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please upload your image")]
        public string NewImage { get; set; }
    }

    public class ChangeProfilePhoto : Settings
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please upload your image")]
        public string NewProfileImage { get; set; }
    }
}