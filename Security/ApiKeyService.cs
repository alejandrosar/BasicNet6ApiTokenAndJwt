using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicNet6Template.Security {

    public interface IApiKeyService
    {
        Task<bool> IsValidApiKeyAsync(string apiKey);
    }

    public class ApiKeyService : IApiKeyService
    {
        private readonly IDictionary<string, string> _apiKeys;

        public ApiKeyService(IEnumerable<KeyValuePair<string, string>> apiKeys)
        {
            _apiKeys = apiKeys.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public async Task<bool> IsValidApiKeyAsync(string apiKey)
        {
            return await Task.Run(() => _apiKeys.Values.Contains(apiKey));
        }
    }

}