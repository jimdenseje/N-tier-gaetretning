using API;
using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using System.Text.Json;

namespace TestProject.Tests
{
    public class Models
    {
        [Fact]
        public void HasAgeGroup()
        {
            var ageGroup = new AgeGroup
            {
                Id = 1,
                MinAge = 0,
                MaxAge = 100
            };

            Assert.Equal(1, ageGroup.Id);
            Assert.Equal(0, ageGroup.MinAge);
            Assert.Equal(100, ageGroup.MaxAge);
        }
    }

    public class AgeGroupTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AgeGroupTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAgeGroups_ReturnsOkWithCorrectNames()
        {
            var response = await _client.GetAsync("/api/AgeGroup");
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var ageGroups = JsonSerializer.Deserialize<List<AgeGroup>>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(ageGroups);
            Assert.Contains(ageGroups, a => a.Name == "Infant");
            Assert.Contains(ageGroups, a => a.Name == "Child");
            Assert.Contains(ageGroups, a => a.Name == "Teenager");
            Assert.Contains(ageGroups, a => a.Name == "Adult");
            Assert.Contains(ageGroups, a => a.Name == "Senior");
        }
    }
}
