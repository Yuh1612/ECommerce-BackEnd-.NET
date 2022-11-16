using ECommerce.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Indentity
{
    public class AllowAnonymousFilter : ActionFilterAttribute
    {
        public AllowAnonymousFilter()
        {
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool hasAllowAnonymous = context.ActionDescriptor
                .EndpointMetadata
                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            if (hasAllowAnonymous)
            {
                var userInfo = context.HttpContext.RequestServices.GetService<IUserInfo>();
                userInfo?.Reset();
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}