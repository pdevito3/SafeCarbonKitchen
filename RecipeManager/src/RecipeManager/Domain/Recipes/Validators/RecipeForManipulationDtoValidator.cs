namespace RecipeManager.Domain.Recipes.Validators
{
    using RecipeManager.Dtos.Recipe;
    using FluentValidation;
    using System;

    public class RecipeForManipulationDtoValidator<T> : AbstractValidator<T> where T : RecipeForManipulationDto
    {
        public RecipeForManipulationDtoValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}