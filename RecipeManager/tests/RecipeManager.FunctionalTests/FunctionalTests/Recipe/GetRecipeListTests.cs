namespace RecipeManager.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetRecipeListTests : TestBase
    {
        [Test]
        public async Task Get_Recipe_List_Returns_200_WithAuth()
        {
            // Arrange
            _client.AddAuth(new[] {"recipes.read"});

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Recipes.GetList);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task Get_Recipe_List_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            // N/A

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Recipes.GetList);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Get_Recipe_List_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            _client.AddAuth();

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Recipes.GetList);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}