using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BugTracker.Api.Dto;
using Xunit;

namespace BugTracker.Api.Tests
{
    public class MilestoneApiTest : IntegrationApiTestBase
    {
        public MilestoneApiTest()
        {
        }
     
        [Fact]
        public async Task GetItemsAsync()
        {
            // Подготовка
            var client = await GetClient();

            // Тестируемое действие
            var response = await client.GetAsync("/api/milestones");

            // Проверка
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Подготовка
            var client = await GetClient();
            var result = await CreateMilestone(client);

            // Тестируемое действие
            var response = await client.GetAsync($"/api/milestones/{result.Id}");

            // Проверка
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task PostAsync()
        {
            // Подготовка
            var client = await GetClient();

            // Тестируемое действие
            var result = await CreateMilestone(client);

            // Проверка
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutAsync()
        {
            // Подготовка
            var client = await GetClient();
            var fromApi = await CreateMilestone(client);

            // Тестируемое действие
            var dataC = new CreateUpdateMilestoneDto
            {
                Description = "Test 1",
                Title = "Test 3" 
            };
            var byteContentC = ConvertToResponce(dataC);
            var response2 = await client.PutAsync($"/api/milestones/{fromApi.Id}", byteContentC);

            // Проверка 
            Assert.True(response2.IsSuccessStatusCode);
            var response3 = await client.GetAsync($"/api/milestones/{fromApi.Id}");
            Assert.True(response3.IsSuccessStatusCode);
            var fromApiChanged = await GetObjectAsync<MilestoneViewDto>(response3);
            Assert.Equal(dataC.Title, fromApiChanged.Title);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Подготовка
            var client = await GetClient();
            MilestoneViewDto fromApi = await CreateMilestone(client);

            // Тестируемое действие
            var response2 = await client.DeleteAsync($"/api/milestones/{fromApi.Id}");

            // Проверка 
            Assert.True(response2.IsSuccessStatusCode);
            var response3 = await client.GetAsync($"/api/milestones/{fromApi.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response3.StatusCode);
        }

        private static async Task<MilestoneViewDto> CreateMilestone(HttpClient client)
        {
            var data = new CreateUpdateMilestoneDto
            {
                Description = "Test 1",
                Title = "Test 1" 
            };
            var byteContent = ConvertToResponce(data);
            var response = await client.PostAsync("/api/milestones", byteContent);
            Assert.True(response.IsSuccessStatusCode);
            var fromApi = await GetObjectAsync<MilestoneViewDto>(response);
            return fromApi;
        }
    }
}
