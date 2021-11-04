using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flurl;
using AuthenticationService;

namespace MyService
{
    public class Authenticator
    {
        ILogger _logger;

        public Authenticator(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<LoginResponse> GetClientLoginToken(string clientToken, string clientSecret)
        {
            var model = new LoginModel() { Identity = clientToken, Password = clientSecret};

            using (var client = new HttpClient())
            {
                var uri = "localhost".AppendPathSegment("api/ClientAuthenticate/login");
                var response = await client.PostAsJsonAsync(uri, model);
                if (!response.IsSuccessStatusCode) throw new ApplicationException("Unable to retrieve access token");
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
        }

    }
}
