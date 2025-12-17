using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FoodOrderingApplicationFigma.Attributes
{
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int[] _roles;

        public AuthorizeRoleAttribute(params int[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == null || !_roles.Contains(int.Parse(userRole)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}