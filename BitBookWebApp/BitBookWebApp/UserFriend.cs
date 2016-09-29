using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class UserFriend : IEnumerable
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int FriendId { get; set; }
        public int Friendstatus { get; set; }

        //this is not added
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}