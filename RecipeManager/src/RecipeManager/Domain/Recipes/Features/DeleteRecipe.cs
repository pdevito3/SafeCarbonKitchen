namespace RecipeManager.Domain.Recipes.Features
{
    using RecipeManager.Domain.Recipes;
    using RecipeManager.Dtos.Recipe;
    using RecipeManager.Exceptions;
    using RecipeManager.Databases;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class DeleteRecipe
    {
        public class DeleteRecipeCommand : IRequest<bool>
        {
            public Guid Id { get; set; }

            public DeleteRecipeCommand(Guid recipe)
            {
                Id = recipe;
            }
        }

        public class Handler : IRequestHandler<DeleteRecipeCommand, bool>
        {
            private readonly RecipeManagerDbcontext _db;
            private readonly IMapper _mapper;

            public Handler(RecipeManagerDbcontext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
            {
                var recordToDelete = await _db.Recipes
                    .FirstOrDefaultAsync(r => r.Id == request.Id);

                if (recordToDelete == null)
                    throw new KeyNotFoundException();

                _db.Recipes.Remove(recordToDelete);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}