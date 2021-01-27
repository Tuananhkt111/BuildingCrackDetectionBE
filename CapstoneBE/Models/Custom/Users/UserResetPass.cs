namespace CapstoneBE.Models.Custom.Users
{
    public class UserResetPass
    {
        public string Token { get; set; }
        public string NewPass { get; set; }
    }
}