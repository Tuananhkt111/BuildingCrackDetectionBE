namespace CapstoneBE.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string JwtEmailEncryption { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}