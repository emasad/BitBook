using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BitBookWebApp.Models;

namespace BitBookWebApp.Context
{
    public class BitBookContext:DbContext
    {
        public BitBookContext() : base("BitBookConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<BasicInfo> BasicInfos { get; set; } 
        public DbSet<UserFriend> UserFriends { get; set; } 

    }
}