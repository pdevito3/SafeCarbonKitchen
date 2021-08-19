namespace RecipeManager.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.IntegrationTests.TestUtilities;
    using RecipeManager.Dtos.Recipe;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System.Linq;
    using RecipeManager.Domain.Recipes.Features;
    using static TestFixture;

    public class UpdateRecipeCommandTests : TestBase
    {
        [Test]
        public async Task UpdateRecipeCommand_Updates_Existing_Recipe_In_Db()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();
            await InsertAsync(fakeRecipeOne);

            var recipe = await ExecuteDbContextAsync(db => db.Recipes.SingleOrDefaultAsync());
            var id = recipe.Id;

            // Act
            var command = new UpdateRecipe.UpdateRecipeCommand(id, updatedRecipeDto);
            await SendAsync(command);
            var updatedRecipe = await ExecuteDbContextAsync(db => db.Recipes.Where(r => r.Id == id).SingleOrDefaultAsync());

            // Assert
            updatedRecipe.Should().BeEquivalentTo(updatedRecipeDto, options =>
                options.ExcludingMissingMembers());
        }
    }
}