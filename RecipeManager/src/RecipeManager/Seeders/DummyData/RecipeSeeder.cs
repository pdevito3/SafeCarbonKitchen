namespace RecipeManager.Seeders.DummyData
{

    using AutoBogus;
    using RecipeManager.Domain.Recipes;
    using RecipeManager.Databases;
    using System.Linq;

    public static class RecipeSeeder
    {
        public static void SeedSampleRecipeData(RecipeManagerDbcontext context)
        {
            if (!context.Recipes.Any())
            {
                context.Recipes.Add(new AutoFaker<Recipe>());
                context.Recipes.Add(new AutoFaker<Recipe>());
                context.Recipes.Add(new AutoFaker<Recipe>());

                context.SaveChanges();
            }
        }
    }
}