using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BugTracker.Api.Dto;
using Xunit;

namespace BugTracker.Api.Tests
{
    public class IssueApiTest : IntegrationApiTestBase
    {
        public IssueApiTest()
        {
        }
     
        [Fact]
        public async Task GetItemsAsync()
        {
            // Подготовка
            var client = await GetClient();

            // Тестируемое действие
            var response = await client.GetAsync("/api/issues");

            // Проверка
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Подготовка
            var client = await GetClient();
            var result = await CreateIssue(client);

            // Тестируемое действие
            var response = await client.GetAsync($"/api/issues/{result.Id}");

            // Проверка
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task PostAsync()
        {
            // Подготовка
            var client = await GetClient();

            // Тестируемое действие
            var result = await CreateIssue(client);

            // Проверка
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutAsync()
        {
            // Подготовка
            var client = await GetClient();
            var fromApi = await CreateIssue(client);

            // Тестируемое действие
            var dataC = new CreateUpdateIssueDto
            {
                Description = "Test 1",
                Subject = "Test 3",
                DetectedVersion = "0.0.1"
            };
            var byteContentC = ConvertToResponce(dataC);
            var response2 = await client.PutAsync($"/api/issues/{fromApi.Id}", byteContentC);

            // Проверка 
            Assert.True(response2.IsSuccessStatusCode);
            var response3 = await client.GetAsync($"/api/issues/{fromApi.Id}");
            Assert.True(response3.IsSuccessStatusCode);
            var fromApiChanged = await GetObjectAsync<IssueViewDto>(response3);
            Assert.Equal(dataC.Subject, fromApiChanged.Subject);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Подготовка
            var client = await GetClient();
            IssueViewDto fromApi = await CreateIssue(client);

            // Тестируемое действие
            var response2 = await client.DeleteAsync($"/api/issues/{fromApi.Id}");

            // Проверка 
            Assert.True(response2.IsSuccessStatusCode);
            var response3 = await client.GetAsync($"/api/issues/{fromApi.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response3.StatusCode);
        }

        private static async Task<IssueViewDto> CreateIssue(HttpClient client)
        {
            var data = new CreateUpdateIssueDto
            {
                Description = "Test 1",
                Subject = "Test 1",
                DetectedVersion = "0.0.1"
            };
            var byteContent = ConvertToResponce(data);
            var response = await client.PostAsync("/api/issues", byteContent);
            Assert.True(response.IsSuccessStatusCode);
            var fromApi = await GetObjectAsync<IssueViewDto>(response);
            return fromApi;
        }
    }
}
