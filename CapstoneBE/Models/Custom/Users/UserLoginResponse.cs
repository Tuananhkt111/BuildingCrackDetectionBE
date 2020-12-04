namespace CapstoneBE.Models.Custom.Users
{
    public class UserLoginResponse
    {
        public string UserId { get; set; }
        public string JwtToken { get; set; }
    }
}