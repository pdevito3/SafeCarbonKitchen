namespace RecipeManager.Domain.Recipes.Features
{
    using RecipeManager.Domain.Recipes;
    using RecipeManager.Dtos.Recipe;
    using RecipeManager.Exceptions;
    using RecipeManager.Databases;
    using RecipeManager.Wrappers;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Sieve.Models;
    using Sieve.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetRecipeList
    {
        public class RecipeListQuery : IRequest<PagedList<RecipeDto>>
        {
            public RecipeParametersDto QueryParameters { get; set; }

            public RecipeListQuery(RecipeParametersDto queryParameters)
            {
                QueryParameters = queryParameters;
            }
        }

        public class Handler : IRequestHandler<RecipeListQuery, PagedList<RecipeDto>>
        {
            private readonly RecipeManagerDbcontext _db;
            private readonly SieveProcessor _sieveProcessor;
            private readonly IMapper _mapper;

            public Handler(RecipeManagerDbcontext db, IMapper mapper, SieveProcessor sieveProcessor)
            {
                _mapper = mapper;
                _db = db;
                _sieveProcessor = sieveProcessor;
            }

            public async Task<PagedList<RecipeDto>> Handle(RecipeListQuery request, CancellationToken cancellationToken)
            {
                if (request.QueryParameters == null)
                    throw new ApiException("Invalid query parameters.");

                var collection = _db.Recipes
                    as IQueryable<Recipe>;

                var sieveModel = new SieveModel
                {
                    Sorts = request.QueryParameters.SortOrder ?? "Id",
                    Filters = request.QueryParameters.Filters
                };

                var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
                var dtoCollection = appliedCollection
                    .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider);

                return await PagedList<RecipeDto>.CreateAsync(dtoCollection,
                    request.QueryParameters.PageNumber,
                    request.QueryParameters.PageSize);
            }
        }
    }
}