namespace RecipeManager.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetRecipeTests : TestBase
    {
        [Test]
        public async Task Get_Recipe_Record_Returns_200_WithAuth()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            _client.AddAuth(new[] {"recipes.read"});
            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.GetRecord.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task Get_Recipe_Record_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.GetRecord.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Get_Recipe_Record_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.GetRecord.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}