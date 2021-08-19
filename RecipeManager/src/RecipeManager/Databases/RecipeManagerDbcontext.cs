namespace RecipeManager.Databases
{
    using RecipeManager.Domain.Recipes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Threading;
    using System.Threading.Tasks;

    public class RecipeManagerDbcontext : DbContext
    {
        public RecipeManagerDbcontext(
            DbContextOptions<RecipeManagerDbcontext> options) : base(options)
        {
        }

        #region DbSet Region - Do Not Delete

        public DbSet<Recipe> Recipes { get; set; }
        #endregion DbSet Region - Do Not Delete

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}