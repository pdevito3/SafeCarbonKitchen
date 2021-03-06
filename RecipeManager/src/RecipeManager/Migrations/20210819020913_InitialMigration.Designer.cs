// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecipeManager.Databases;

namespace RecipeManager.Migrations
{
    [DbContext(typeof(RecipeManagerDbcontext))]
    [Migration("20210819020913_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("RecipeManager.Domain.Recipes.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("Ethnicity")
                        .HasColumnType("text")
                        .HasColumnName("ethnicity");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("initials");

                    b.Property<string>("Race")
                        .HasColumnType("text")
                        .HasColumnName("race");

                    b.HasKey("Id");

                    b.ToTable("recipe");
                });
#pragma warning restore 612, 618
        }
    }
}
