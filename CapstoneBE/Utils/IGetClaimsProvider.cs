namespace TranslatorAPI.Utils
{
    public interface IGetClaimsProvider
    {
        string UserId { get; }
        string Role { get; }
        int[] LocationIds { get; set; }
    }
}