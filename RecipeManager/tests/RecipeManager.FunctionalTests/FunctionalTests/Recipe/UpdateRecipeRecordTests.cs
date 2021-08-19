namespace RecipeManager.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdateRecipeRecordTests : TestBase
    {
        [Test]
        public async Task Put_Recipe_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();

            _client.AddAuth(new[] {"recipes.update"});

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Put.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedRecipeDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task Put_Recipe_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Put.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedRecipeDto);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Put_Recipe_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Put.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedRecipeDto);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}