namespace RecipeManager.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using RecipeManager.Domain.Recipes.Features;
    using static TestFixture;
    using System;
    using RecipeManager.Exceptions;

    public class AddRecipeCommandTests : TestBase
    {
        [Test]
        public async Task AddRecipeCommand_Adds_New_Recipe_To_Db()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipeForCreationDto { }.Generate();

            // Act
            var command = new AddRecipe.AddRecipeCommand(fakeRecipeOne);
            var recipeReturned = await SendAsync(command);
            var recipeCreated = await ExecuteDbContextAsync(db => db.Recipes.SingleOrDefaultAsync());

            // Assert
            recipeReturned.Should().BeEquivalentTo(fakeRecipeOne, options =>
                options.ExcludingMissingMembers());
            recipeCreated.Should().BeEquivalentTo(fakeRecipeOne, options =>
                options.ExcludingMissingMembers());
        }
    }
}