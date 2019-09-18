using sys.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace sys.WebApi.Controllers
{
    /// <summary>
    /// TestAPIController
    /// </summary>
    public class TestAPIController : ApiController
    {
        /// <summary>
        /// SetUserInfo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SetUserInfo(UserInfo dto)
        {
            UserInfoRestule info = new UserInfoRestule();
            info.UserId = dto.UserId;
            info.UserName = dto.UserName;
            info.Age = 18;
            info.Email = "xxxxx@xx.com";
            return Json<UserInfoRestule>(info);
        }
        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult GetUser(UserInfo dto)
        {
            UserInfoRestule info = new UserInfoRestule(); 
            info.Age = 18;
            info.Email = "xxxxx@xx.com";
            return Json<UserInfoRestule>(info);
        }
        /// <summary>
        /// GetUser1
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        //[ApiExplorerSettings(IgnoreApi = true)] 
        public IHttpActionResult GetUser1(UserInfo dto)
        {
            UserInfoRestule info = new UserInfoRestule();
            info.Age = 18;
            info.Email = "xxxxx@xx.com";
            return Json<UserInfoRestule>(info);
        }
    }
}
