namespace RecipeManager.Dtos.Recipe
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class RecipeForManipulationDto 
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}