namespace RecipeManager.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DeleteRecipeTests : TestBase
    {
        [Test]
        public async Task Delete_RecipeReturns_NoContent_WithAuth()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            _client.AddAuth(new[] {"recipes.delete"});
            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Delete.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task Delete_Recipe_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Delete.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Delete_Recipe_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Delete.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}