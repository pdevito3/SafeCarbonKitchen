namespace RecipeManager.Domain.Recipes.Mappings
{
    using RecipeManager.Dtos.Recipe;
    using AutoMapper;
    using RecipeManager.Domain.Recipes;

    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            //createmap<to this, from this>
            CreateMap<Recipe, RecipeDto>()
                .ReverseMap();
            CreateMap<RecipeForCreationDto, Recipe>();
            CreateMap<RecipeForUpdateDto, Recipe>()
                .ReverseMap();
        }
    }
}