namespace RecipeManager.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManager.Dtos.Recipe;
    using RecipeManager.SharedTestHelpers.Fakes.Recipe;
    using RecipeManager.Exceptions;
    using RecipeManager.Domain.Recipes.Features;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class RecipeListQueryTests : TestBase
    {
        
        [Test]
        public async Task RecipeListQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            var queryParameters = new RecipeParametersDto();

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            // Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(2);
        }
        
        [Test]
        public async Task RecipeListQuery_Returns_Expected_Page_Size_And_Number()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            var fakeRecipeThree = new FakeRecipe { }.Generate();
            var queryParameters = new RecipeParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo, fakeRecipeThree);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
        }
        
        [Test]
        public async Task RecipeListQuery_Throws_ApiException_When_Null_Query_Parameters()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetRecipeList.RecipeListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Name_List_In_Asc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Name = "bravo";
            fakeRecipeTwo.Name = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Name" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Name_List_In_Desc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Name = "bravo";
            fakeRecipeTwo.Name = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Name" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Age_List_In_Asc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Age = 2;
            fakeRecipeTwo.Age = 1;
            var queryParameters = new RecipeParametersDto() { SortOrder = "Age" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Age_List_In_Desc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Age = 2;
            fakeRecipeTwo.Age = 1;
            var queryParameters = new RecipeParametersDto() { SortOrder = "Age" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Race_List_In_Asc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Race = "bravo";
            fakeRecipeTwo.Race = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Race" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Race_List_In_Desc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Race = "bravo";
            fakeRecipeTwo.Race = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Race" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Ethnicity_List_In_Asc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Ethnicity = "bravo";
            fakeRecipeTwo.Ethnicity = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Ethnicity" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Returns_Sorted_Recipe_Ethnicity_List_In_Desc_Order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Ethnicity = "bravo";
            fakeRecipeTwo.Ethnicity = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Ethnicity" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task RecipeListQuery_Filters_Recipe_Name()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Name = "alpha";
            fakeRecipeTwo.Name = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"Name == {fakeRecipeTwo.Name}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Filters_Recipe_Age()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Age = 1;
            fakeRecipeTwo.Age = 2;
            var queryParameters = new RecipeParametersDto() { Filters = $"Age == {fakeRecipeTwo.Age}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Filters_Recipe_Race()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Race = "alpha";
            fakeRecipeTwo.Race = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"Race == {fakeRecipeTwo.Race}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task RecipeListQuery_Filters_Recipe_Ethnicity()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Ethnicity = "alpha";
            fakeRecipeTwo.Ethnicity = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"Ethnicity == {fakeRecipeTwo.Ethnicity}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}