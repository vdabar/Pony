using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Pony.Data.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor, PonyDbContext dbContext)
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return new Guid();
                //return dbContext.Users
                //    .Where(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name)
                //    .Select(x => x.Id)
                //    .FirstOrDefault();
            }

            return Guid.Empty;
        }
    }
}