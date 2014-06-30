using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using Blog.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace Blog
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount("admin", "admin123");
            }
            if (!WebSecurity.UserExists("writer"))
            {
                WebSecurity.CreateUserAndAccount("writer", "writer123");
            }     
            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin"); 
            }
            if (!Roles.RoleExists("Writer"))
            {
                Roles.CreateRole("Writer");
            }            
            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }
            if (!Roles.IsUserInRole("admin", "Admin"))
            {
                Roles.AddUserToRole("admin", "Admin");
            }
            if (!Roles.IsUserInRole("writer","Writer"))
            {
                Roles.AddUserToRole("writer", "Writer");
            }
            

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
