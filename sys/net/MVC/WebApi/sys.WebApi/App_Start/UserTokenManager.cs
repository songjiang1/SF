using sys.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sys.WebApi
{
    //参考文档 https://www.cnblogs.com/niuww/p/5639637.html
    #region  登录处理
    /// // token  登录 处理
    ///              UserTokenManager.RemoveToken(u.ID);
    ///              // 生成新Token
    ///              var token = Utility.MD5Encrypt(string.Format("{0}{1}", Guid.NewGuid().ToString("D"), DateTime.Now.Ticks));
    ///              // token过期时间
    ///              int timeout = 8;
    ///              if (!int.TryParse(ConfigurationManager.AppSettings["TokenTimeout"], out timeout))
    ///                  timeout = 8;
    ///              // 创建新token
    ///              var ut = new UserToken()
    ///              {
    ///                  Token = token,
    ///                  Timeout = DateTime.Now.AddHours(timeout),
    ///                  UId = u.ID,
    ///                  UserType = (u.IsSaler.HasValue && u.IsSaler.Value) ? "Saler" : "Vip"
    ///              };
    ///
    ///              UserTokenManager.AddToken(ut);
    #endregion
    //UserTokenManager.RemoveToken(this.Token);
    #region  退出处理
    /// // token  登录 处理
    ///              UserTokenManager.RemoveToken(u.ID);
    ///              // 生成新Token
    ///              var token = Utility.MD5Encrypt(string.Format("{0}{1}", Guid.NewGuid().ToString("D"), DateTime.Now.Ticks));
    ///              // token过期时间
    ///              int timeout = 8;
    ///              if (!int.TryParse(ConfigurationManager.AppSettings["TokenTimeout"], out timeout))
    ///                  timeout = 8;
    ///              // 创建新token
    ///              var ut = new UserToken()
    ///              {
    ///                  Token = token,
    ///                  Timeout = DateTime.Now.AddHours(timeout),
    ///                  UId = u.ID,
    ///                  UserType = (u.IsSaler.HasValue && u.IsSaler.Value) ? "Saler" : "Vip"
    ///              };
    ///
    ///              UserTokenManager.AddToken(ut);
    #endregion

    #region  ajax调用
    //TestAuthorize 1
    //function TestAuthorize1()
    //{
    //$.ajax({
    //        type: "get",
    //    url: host + "/mobileapi/test/TestAuthorize1",
    //    dataType: "text",
    //    data: { },
    //    beforeSend: function(request) {
    //            request.setRequestHeader("token", $("#token").val());  // 请求发起前在头部附加token
    //        },
    //    success: function(data) {
    //            alert(data);
    //        },
    //    error: function(x, y, z) {
    //            alert("报错无语");
    //        }
    //    });
    //}

 
    #endregion
    /// <summary>
    /// 需要连接数据库
    ///  
    /// </summary>
    public class UserTokenManager
    {
        //private static readonly IUserTokenRepository _tokenRep;
        private const string TOKENNAME = "PASSPORT.TOKEN";

        static UserTokenManager()
        {
            //_tokenRep = ContainerManager.Resolve<IUserTokenRepository>();
        }
        /// <summary>
        /// 初始化缓存
        /// </summary>
        private static List<Token> InitCache()
        {
            if (HttpRuntime.Cache[TOKENNAME] == null)
            {
                var tokens = _tokenRep.GetAll();
                // cache 的过期时间， 令牌过期时间 *2
                HttpRuntime.Cache.Insert(TOKENNAME, tokens, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromDays(7 * 2));
            }
            var ts = (List<Token>)HttpRuntime.Cache[TOKENNAME];
            return ts;
        }


        public static int GetUId(string token)
        {
            var tokens = InitCache();
            var result = 0;
            if (tokens.Count > 0)
            {
                var id = tokens.Where(c => c.Token == token).Select(c => c.UId).FirstOrDefault();
                if (id != null)
                    result = id.Value;
            }
            return result;
        }


        public static string GetPermission(string token)
        {
            var tokens = InitCache();
            if (tokens.Count == 0)
                return "NoAuthorize";
            else
                return tokens.Where(c => c.Token == token).Select(c => c.Permission).FirstOrDefault();
        }

        public static string GetUserType(string token)
        {
            var tokens = InitCache();
            if (tokens.Count == 0)
                return "";
            else
                return tokens.Where(c => c.Token == token).Select(c => c.UserType).FirstOrDefault();
        }

        /// <summary>
        /// 判断令牌是否存在
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsExistToken(string token)
        {
            var tokens = InitCache();
            if (tokens.Count == 0) return false;
            else
            {
                var t = tokens.Where(c => c.Token == token).FirstOrDefault();
                if (t == null)
                    return false;
                else if (t.Timeout < DateTime.Now)
                {
                    RemoveToken(t);
                    return false;
                }
                else
                {
                    // 小于8小时 更新过期时间
                    if ((t.Timeout - DateTime.Now).TotalMinutes < 1 * 60 - 1)
                    {
                        t.Timeout = DateTime.Now.AddHours(8);
                        UpdateToken(t);
                    }
                    return true;
                }

            }
        }

        /// <summary>
        /// 添加令牌， 没有则添加，有则更新
        /// </summary>
        /// <param name="token"></param>
        public static void AddToken(Token token)
        {
            var tokens = InitCache();
            // 不存在  怎增加
            if (!IsExistToken(token.Token))
            {
                token.ID = 0;
                tokens.Add(token);
                // 插入数据库
                _tokenRep.Add(token);
            }
            else  // 有则更新
            {
                UpdateToken(token);
            }
        }

        public static bool UpdateToken(Token token)
        {
            var tokens = InitCache();
            if (tokens.Count == 0) return false;
            else
            {
                var t = tokens.Where(c => c.Token == token.Token).FirstOrDefault();
                if (t == null)
                    return false;
                t.Timeout = token.Timeout;
                // 更新数据库
                var tt = _tokenRep.FindByToken(token.Token);
                if (tt != null)
                {
                    tt.UserType = token.UserType;
                    tt.UId = token.UId;
                    tt.Permission = token.Permission;
                    tt.Timeout = token.Timeout;
                    _tokenRep.Update(tt);
                }
                return true;
            }
        }
        /// <summary>
        /// 移除指定令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static void RemoveToken(Token token)
        {
            var tokens = InitCache();
            if (tokens.Count == 0) return;
            tokens.Remove(token);
            _tokenRep.Remove(token);
        }

        public static void RemoveToken(string token)
        {
            var tokens = InitCache();
            if (tokens.Count == 0) return;

            var ts = tokens.Where(c => c.Token == token).ToList();
            foreach (var t in ts)
            {
                tokens.Remove(t);
                var tt = _tokenRep.FindByToken(t.Token);
                if (tt != null)
                    _tokenRep.Remove(tt);
            }
        }


        public static void RemoveToken(int uid)
        {
            var tokens = InitCache();
            if (tokens.Count == 0) return;

            var ts = tokens.Where(c => c.UId == uid).ToList();
            foreach (var t in ts)
            {
                tokens.Remove(t);
                var tt = _tokenRep.FindByToken(t.Token);
                if (tt != null)
                    _tokenRep.Remove(tt);
            }
        }
    }
}