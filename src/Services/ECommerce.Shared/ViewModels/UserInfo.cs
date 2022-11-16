namespace ECommerce.Shared.ViewModels
{
    public interface IUserInfo
    {
        Guid Id { get; set; }
        string? Username { get; set; }
        string? AvatarUrl { get; set; }
        int RoleId { get; set; }
        string[]? Scopes { get; set; }

        void Reset();
    }

    public class UserInfo : IUserInfo
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? AvatarUrl { get; set; }
        public int RoleId { get; set; }
        public string[]? Scopes { get; set; }

        public void Reset()
        {
            Id = default;
            Username = default;
            AvatarUrl = default;
            RoleId = default;
            Scopes = default;
        }
    }
}