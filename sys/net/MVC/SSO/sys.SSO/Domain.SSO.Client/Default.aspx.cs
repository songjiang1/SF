﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Domain.SSO.Client
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tokenID = string.Empty;
            if (!string.IsNullOrEmpty(Request["token"]))
            {
                tokenID = Request["token"];
                HttpCookie tokenCookie = new HttpCookie("token", tokenID);
                tokenCookie.HttpOnly = true;
                Response.Cookies.Set(tokenCookie);
            }
            else if (Request.Cookies["token"] != null)
            {
                tokenID = Request.Cookies["token"].Value;
            }
            
            if (!string.IsNullOrEmpty(tokenID))
            {
                AuthTokenService.AuthTokenServiceSoapClient client = new AuthTokenService.AuthTokenServiceSoapClient();
                var token = client.ValidateToken(tokenID);
                if (token != null)
                {
                    this.lblMessage.Text = "登录成功，登录用户："
                        + token.User.UserName
                        + "<a href='http://localhost:17311/logout.aspx?returnUrl="
                        + Server.UrlEncode("http://localhost:3217/")
                        + "'>退出</a>";
                }
                else
                {
                    Response.Redirect("http://localhost:17311/sso.aspx?returnUrl=" +
                        Server.UrlEncode("http://localhost:3217/default.aspx"));
                }
            }
            else
            {
                Response.Redirect("http://localhost:17311/sso.aspx?returnUrl=" +
                    Server.UrlEncode("http://localhost:3217/default.aspx"));
            }
        }
    }
}