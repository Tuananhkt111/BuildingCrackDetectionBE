namespace CapstoneBE.Utils
{
    //Extract claims information from JWT
    public interface IGetClaimsProvider
    {
        public string UserId { get; }
        public string Role { get; }
    }
}