using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitBookWebApp.Models
{
    public class User
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide your name")]
        [StringLength(10, MinimumLength = 3)]
        [DisplayName("Name")]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Please provide your name")]
        [StringLength(10, MinimumLength = 3)]
        [DisplayName("SurName")]
        [Column(TypeName = "varchar")]
        public string SurName { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(450)]
        [Required]
        [Remote("IsEmailExist", "Registration", ErrorMessage = "Email Already Exist.")]
        [DisplayName("Email")]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])", ErrorMessage = "Please provide valid email")]
        public string Email { get; set; }


        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        //[Required]
        //[DataType(DataType.MultilineText)]
        //public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }


    public class LogViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}