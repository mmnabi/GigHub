using System.Security.Claims;

namespace GigHub.Utilities.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue("Id");
        }
    }
}
