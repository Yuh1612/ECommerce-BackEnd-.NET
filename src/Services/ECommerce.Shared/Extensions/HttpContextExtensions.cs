using ECommerce.Shared.Constants;
using ECommerce.Shared.ViewModels;
using Microsoft.AspNetCore.Http;

namespace ELDesk.Shared.Infrastructure.Service.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetClaimValue(HttpContext context, string claimName)
        {
            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                return context.User.Claims.FirstOrDefault(x => x.Type == claimName)?.Value;
            }
            return default;
        }

        public static int UserId(this HttpContext context)
        {
            int.TryParse(GetClaimValue(context, AppClaimType.UserId), out var userId);
            return userId;
        }

        public static int RoleId(this HttpContext context)
        {
            int.TryParse(GetClaimValue(context, AppClaimType.RoleId), out var roleId);
            return roleId;
        }

        public static string? Username(this HttpContext context)
        {
            return GetClaimValue(context, AppClaimType.Username);
        }

        public static string? AvatarUrl(this HttpContext context)
        {
            return GetClaimValue(context, AppClaimType.AvatarUrl);
        }

        public static string[]? Scopes(this HttpContext context)
        {
            var scopesStr = GetClaimValue(context, AppClaimType.Scopes);
            return string.IsNullOrEmpty(scopesStr) ? null : scopesStr.Split(',');
        }

        public static string? AccessToken(this HttpContext context)
        {
            return GetToken(context?.Request?.Headers?["Authorization"]);
        }

        private static string? GetToken(string? authorizationHeader)
        {
            if (authorizationHeader == null) return null;
            var arr = authorizationHeader.Split(" ");
            if (arr.Length != 2 || arr[1] == "null") return null;
            return arr[1];
        }

        public static IUserInfo CurrentUser(this HttpContext context)
        {
            return new UserInfo
            {
                Id = context.UserId(),
                Username = context.Username(),
                AvatarUrl = context.AvatarUrl(),
                RoleId = context.RoleId(),
                Scopes = context.Scopes()
            };
        }
    }
}