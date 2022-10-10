namespace ECommerce.Shared.Constants
{
    public struct DefaultPaging
    {
        public const int DEFAULT_PAGE_NO = 1;
        public const int DEFAULT_PAGE_SIZE = 20;
    }

    public struct AppClaimType
    {
        public const string UserId = "Id";
        public const string RoleId = "RoleId";
        public const string Username = "Username";
        public const string AvatarUrl = "AvatarUrl";
        public const string Scopes = "Scopes";
        public const string TimezoneOffset = "TimezoneOffset";
        public const string IsKeepSignedIn = "IsKeepSignedIn";
        public const string ExternalToken = "ExternalToken";
        public const string ExternalProvider = "ExternalProvider";
        public const string JwtId = "jti";
        public const string DeviceAgent = "DeviceAgent";
    }
}