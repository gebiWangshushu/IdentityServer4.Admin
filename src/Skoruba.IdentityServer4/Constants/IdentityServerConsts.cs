using Skoruba.IdentityServer4.Common;

namespace Skoruba.IdentityServer4.Constants
{
    public class IdentityServerConsts
    {
        public const string AdministrationRole = "SkorubaIdentityAdminAdministrator";
        public static string IdentityAdminBaseUrl = Config.Get("IdentityAdminBaseUrl");// "http://localhost:9000";     
        public const string OidcClientId = "skoruba_identity_admin";
        public const string OidcClientName = "Skoruba Identity Admin";
        public const string AdminUserName = "admin";
        public const string AdminPassword = "Pa$$word123";
    }
}