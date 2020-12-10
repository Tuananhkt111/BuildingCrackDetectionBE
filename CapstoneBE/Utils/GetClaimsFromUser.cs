using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace TranslatorAPI.Utils
{
    public class GetClaimsFromUser : IGetClaimsProvider
    {
        public string UserId { get; }
        public string Role { get; }
        public int[] LocationIds { get; set; }

        public GetClaimsFromUser(IHttpContextAccessor accessor)
        {
            UserId = accessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Role = accessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            string[] locationValues = accessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.UserData)?.Value.Split(';');
            if (locationValues != null && locationValues.Length > 0)
            {
                LocationIds = new int[locationValues.Length];
                for (int i = 0; i < locationValues.Length; i++)
                {
                    _ = int.TryParse(locationValues[i], out LocationIds[i]);
                }
            }
        }
    }
}