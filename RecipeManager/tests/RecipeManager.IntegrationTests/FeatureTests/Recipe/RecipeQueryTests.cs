namespace RecipeManager.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipeManager.Domain.Recipes.Features;
    using static TestFixture;

    public class RecipeQueryTests : TestBase
    {
        [Test]
        public async Task RecipeQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            await InsertAsync(fakeRecipeOne);

            // Act
            var query = new GetRecipe.RecipeQuery(fakeRecipeOne.Id);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().BeEquivalentTo(fakeRecipeOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeQuery_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetRecipe.RecipeQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}