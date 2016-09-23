using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookWebApp.Context;
using BitBookWebApp.Models;

namespace BitBookWebApp.BitBook.Core.DAL
{
    public class RegistrationGateway
    {

        public bool SaveUserRegistraion(User aUser)
        {
            bool isSaved = false;
            DateTime temp;

            if (aUser != null && aUser.FirstName != null && aUser.Email != null && aUser.Password != null && aUser.ConfirmPassword != null && aUser.Gender != null && DateTime.TryParse(aUser.DateOfBirth.ToString(), out temp))
            {
                BitBookContext _db = new BitBookContext();

                _db.Users.Add(aUser);
                int rowAffected = _db.SaveChanges();
                isSaved = rowAffected > 0;
            }

            return isSaved;
        }

        public bool IsEmailAleadyExist(string email)
        {
            bool isValidate = false;

            BitBookContext _db = new BitBookContext();

            if (_db.Users.Any(x => x.Email.Equals(email)))
            {
                isValidate = true;
            }

            return isValidate;
        }
    }
}