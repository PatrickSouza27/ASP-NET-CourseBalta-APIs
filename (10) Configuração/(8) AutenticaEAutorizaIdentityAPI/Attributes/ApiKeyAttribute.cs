using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _8__AutenticaEAutorizaIdentityAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Query.TryGetValue(Configuration.ApiKeyName, out var apiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey Não Encontrada"
                };
                return;
            }

            if(!Configuration.ApiKey.Equals(apiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Acesso não Autorizado"
                };
                return;
            }
            await next();
        }
    }
}
