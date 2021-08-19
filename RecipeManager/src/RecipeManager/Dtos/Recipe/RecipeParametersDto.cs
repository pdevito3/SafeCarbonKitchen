namespace RecipeManager.Dtos.Recipe
{
    using RecipeManager.Dtos.Shared;

    public class RecipeParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}