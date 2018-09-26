﻿using Skoruba.IdentityServer4.Common;
using System.Collections.Generic;

namespace Skoruba.IdentityServer4.Admin.Constants
{
    public class AuthorizationConsts
    {
        public const string AdministrationPolicy = "RequireAdministratorRole";
        public const string AdministrationRole = "SkorubaIdentityAdminAdministrator";

        public const string IdentityAdminCookieName = "IdentityServerAdmin";
        public static string IdentityAdminRedirectUri = $"{Config.Get("IdentityAdminBaseUrl")}/signin-oidc";
        public static string IdentityServerBaseUrl = Config.Get("IdentityServerBaseUrl");// "http://localhost:5000";
        public static string IdentityAdminBaseUrl = Config.Get("IdentityAdminBaseUrl");//"http://localhost:9000";

        public const string UserNameClaimType = "name";
        public const string SignInScheme = "Cookies";
        public const string OidcClientId = "skoruba_identity_admin";
        public const string OidcAuthenticationScheme = "oidc";
        public const string OidcResponseType = "id_token";
        public static List<string> Scopes = new List<string> { "openid", "profile", "email", "roles" };
        
        public const string ScopeOpenId = "openid";
        public const string ScopeProfile = "profile";
        public const string ScopeEmail = "email";
        public const string ScopeRoles = "roles";

        public const string AccountLoginPage = "Account/Login";
        public const string AccountAccessDeniedPage = "/Account/AccessDenied/";
    }
}