using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sys.WebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    ///
    public class UserInfoRestule : UserInfo
    {
        public int Age { get; set; }
        public string Email { get; set; }
    } 
}