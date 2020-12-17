using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;
using static IdentityModel.OidcConstants;

namespace BugTracker.Api.Tests
{
    public class IntegrationApiTestBase
    {
        private HttpClient _cached = null;

        public IntegrationApiTestBase()
        {
            
        }
  
        [Fact]
        public Task GetClientTest()
        {
            return GetClient();
        }

        [Fact]
        public Task GetAccessTokenAsyncTest()
        {
            return GetAccessTokenAsync();
        }

        protected static async Task<T> GetObjectAsync<T>(HttpResponseMessage response)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }


        protected static ByteArrayContent ConvertToResponce(object data)
        {
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        protected async Task<HttpClient> GetClient()
        {
            if(_cached != null)
                return _cached;
            var host = await GetHost();
            var tokenResponse = await GetAccessTokenAsync();
            var client = host.GetTestClient();
            client.SetBearerToken(tokenResponse);
            _cached = client;
            return client;
        }

        protected static async Task<IHost> GetHost()
        {
            return await new HostBuilder()
                                .ConfigureWebHost(webBuilder =>
                                {
                                    webBuilder.UseTestServer().UseStartup<Startup>();
                                })
                                .StartAsync();
        }

        protected static async Task<string> GetAccessTokenAsync()
        {
            HttpClient httpClient = new HttpClient();
            DiscoveryDocumentResponse discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("http://localhost:4000");
            PasswordTokenRequest passwordTokenRequest = new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "interactive",
                ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                GrantType = GrantTypes.ClientCredentials,
                Scope = "bugtracker.api",
                UserName = "alice",
                Password = "Pass123$"
            };
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
            Assert.False(tokenResponse.IsError, "Запустите айдентити сервер - dotnet run --urls=http://localhost:4000");
            Assert.NotNull(tokenResponse.AccessToken);
            return tokenResponse.AccessToken;
        }
    }
}
