namespace RecipeManager.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using RecipeManager.Domain.Recipes.Features;
    using static TestFixture;

    public class DeleteRecipeCommandTests : TestBase
    {
        [Test]
        public async Task DeleteRecipeCommand_Deletes_Recipe_From_Db()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            await InsertAsync(fakeRecipeOne);
            var recipe = await ExecuteDbContextAsync(db => db.Recipes.SingleOrDefaultAsync());
            var id = recipe.Id;

            // Act
            var command = new DeleteRecipe.DeleteRecipeCommand(id);
            await SendAsync(command);
            var recipes = await ExecuteDbContextAsync(db => db.Recipes.ToListAsync());

            // Assert
            recipes.Count.Should().Be(0);
        }

        [Test]
        public async Task DeleteRecipeCommand_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteRecipe.DeleteRecipeCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}