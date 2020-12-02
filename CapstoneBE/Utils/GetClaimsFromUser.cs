using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace CapstoneBE.Utils
{
    //Extract claims information from JWT
    public class GetClaimsFromUser : IGetClaimsProvider
    {
        public string UserId { get; }
        public string Role { get; }

        public GetClaimsFromUser(IHttpContextAccessor accessor)
        {
            UserId = accessor.HttpContext?.User.Claims.SingleOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            Role = accessor.HttpContext?.User.Claims.SingleOrDefault(u => u.Type == ClaimTypes.Role)?.Value;
        }
    }
}