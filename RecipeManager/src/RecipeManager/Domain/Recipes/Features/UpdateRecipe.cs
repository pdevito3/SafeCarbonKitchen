namespace RecipeManager.Domain.Recipes.Features
{
    using RecipeManager.Domain.Recipes;
    using RecipeManager.Dtos.Recipe;
    using RecipeManager.Exceptions;
    using RecipeManager.Databases;
    using RecipeManager.Domain.Recipes.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class UpdateRecipe
    {
        public class UpdateRecipeCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public RecipeForUpdateDto RecipeToUpdate { get; set; }

            public UpdateRecipeCommand(Guid recipe, RecipeForUpdateDto newRecipeData)
            {
                Id = recipe;
                RecipeToUpdate = newRecipeData;
            }
        }

        public class Handler : IRequestHandler<UpdateRecipeCommand, bool>
        {
            private readonly RecipeManagerDbcontext _db;
            private readonly IMapper _mapper;

            public Handler(RecipeManagerDbcontext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
            {
                var recipeToUpdate = await _db.Recipes
                    .FirstOrDefaultAsync(r => r.Id == request.Id);

                if (recipeToUpdate == null)
                    throw new KeyNotFoundException();

                _mapper.Map(request.RecipeToUpdate, recipeToUpdate);

                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}