using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;


namespace BasicNet6Template.Security{

    public class ApiKeyAuthorizeAttribute : TypeFilterAttribute
    {
        public ApiKeyAuthorizeAttribute() : base(typeof(ApiKeyAuthorizeFilter))
        {
        }

        private class ApiKeyAuthorizeFilter : IAsyncAuthorizationFilter
        {
            private const string ApiKeyHeaderName = "X-Api-Key";
            private readonly IApiKeyService _apiKeyService;

            public ApiKeyAuthorizeFilter(IApiKeyService apiKeyService)
            {
            _apiKeyService = apiKeyService;
            }
            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValue))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                if (!await _apiKeyService.IsValidApiKeyAsync(apiKeyHeaderValue))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}