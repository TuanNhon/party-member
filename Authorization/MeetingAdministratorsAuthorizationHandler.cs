using System.Threading.Tasks;
using WebApp1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebApp1.Authorization
{
    public class MeetingAdministratorsAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Meeting>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Meeting resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.MeetingAdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
