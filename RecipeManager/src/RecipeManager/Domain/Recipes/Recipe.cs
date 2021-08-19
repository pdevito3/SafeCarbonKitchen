namespace RecipeManager.Domain.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Sieve.Attributes;

    [Table("recipe")]
    public class Recipe
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Sieve(CanFilter = true, CanSort = true)]
        [Column("initials")]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [Column("age")]
        public int Age { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [Column("race")]
        public string Race { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [Column("ethnicity")]
        public string Ethnicity { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}