using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BugTracker.Api.Dto;
using Xunit;

namespace BugTracker.Api.Tests
{
    public class IssueAdittionalApiTest : IntegrationApiTestBase
    {
        public IssueAdittionalApiTest()
        {
        }

        [Fact]
        public async Task PostBigDescriptionAsync()
        {
            // Подготовка
            var client = await GetClient();

            // Тестируемое действие
            var c = await CreateIssue(client, new string('d', 2000));

            // Проверка
            Assert.False(c.IsSuccessStatusCode);
        }

        
        [Fact(Timeout = 1200)]
        public async Task PostManyAsync()
        {
            // Подготовка
            var client = await GetClient();
                
            // Тестируемое действие
            for (int i = 0; i < 100; i++)
            {
                await CreateIssue(client, new string('d', 1000));
            }
          
            // Проверка
        }
 
        private static async Task<HttpResponseMessage> CreateIssue(HttpClient client, string decsription)
        {
            var data = new CreateUpdateIssueDto
            {
                Description = decsription,
                Subject = "Test 1",
                DetectedVersion = "0.0.1"
            };
            var byteContent = ConvertToResponce(data);
            return  await client.PostAsync("/api/issues", byteContent);
        }
    }
}
