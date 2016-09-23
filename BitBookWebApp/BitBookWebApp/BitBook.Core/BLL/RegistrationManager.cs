using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookWebApp.BitBook.Core.DAL;
using BitBookWebApp.Models;

namespace BitBookWebApp.BitBook.Core.BLL
{
    public class RegistrationManager
    {
        RegistrationGateway aRegistrationGateway= new RegistrationGateway();

        public bool SaveUserRegistraion(User aUser)
        {
            return aRegistrationGateway.SaveUserRegistraion(aUser);
        }

        public bool IsEmailAleadyExist(string email)
        {
            return aRegistrationGateway.IsEmailAleadyExist(email);
        }
    }
}