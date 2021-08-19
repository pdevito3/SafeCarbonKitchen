namespace RecipeManager.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreateRecipeTests : TestBase
    {
        [Test]
        public async Task Create_Recipe_Returns_Created_WithAuth()
        {
            // Arrange
            var fakeRecipe = new FakeRecipeForCreationDto { }.Generate();

            _client.AddAuth(new[] {"recipes.add"});

            // Act
            var route = ApiRoutes.Recipes.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeRecipe);

            // Assert
            result.StatusCode.Should().Be(201);
        }
            
        [Test]
        public async Task Create_Recipe_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeRecipe);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Create_Recipe_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeRecipe);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}