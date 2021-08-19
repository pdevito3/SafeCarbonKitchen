namespace RecipeManager.IntegrationTests
{
    using RecipeManager.Databases;
    using RecipeManager.IntegrationTests.TestUtilities;
    using RecipeManager;
    using MediatR;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Npgsql;
    using NUnit.Framework;
    using Respawn;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [SetUpFixture]
    public class TestFixture
    {
        private static IConfigurationRoot _configuration;
        private static IWebHostEnvironment _env;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        private string _dockerContainerId;
        private string _dockerSqlPort;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            (_dockerContainerId, _dockerSqlPort) = await DockerSqlDatabaseUtilities.EnsureDockerStartedAndGetContainerIdAndPortAsync();
            var dockerConnectionString = DockerSqlDatabaseUtilities.GetSqlConnectionString(_dockerSqlPort);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "UseInMemoryDatabase", "false" },
                        { "ConnectionStrings:RecipeManager", dockerConnectionString }
                    })
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            _env = Mock.Of<IWebHostEnvironment>();

            var startup = new Startup(_configuration, _env);

            var services = new ServiceCollection();

            services.AddLogging();

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" },
                SchemasToExclude = new[] { "information_schema", "pg_subscription", "pg_catalog", "pg_toast" },
                DbAdapter = DbAdapter.Postgres
            };

            EnsureDatabase();

            // MassTransit Setup -- Do Not Delete Comment
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<RecipeManagerDbcontext>();

            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task ResetState()
        {
            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("RecipeManager")))
            {
                await conn.OpenAsync();
                await _checkpoint.Reset(conn);
            }
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<RecipeManagerDbcontext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<RecipeManagerDbcontext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RecipeManagerDbcontext>();

            try
            {
                //await dbContext.BeginTransactionAsync();

                await action(scope.ServiceProvider);

                //await dbContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                //dbContext.RollbackTransaction();
                throw;
            }
        }

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RecipeManagerDbcontext>();

            try
            {
                //await dbContext.BeginTransactionAsync();

                var result = await action(scope.ServiceProvider);

                //await dbContext.CommitTransactionAsync();

                return result;
            }
            catch (Exception)
            {
                //dbContext.RollbackTransaction();
                throw;
            }
        }

        public static Task ExecuteDbContextAsync(Func<RecipeManagerDbcontext, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<RecipeManagerDbcontext>()));

        public static Task ExecuteDbContextAsync(Func<RecipeManagerDbcontext, ValueTask> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<RecipeManagerDbcontext>()).AsTask());

        public static Task ExecuteDbContextAsync(Func<RecipeManagerDbcontext, IMediator, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<RecipeManagerDbcontext>(), sp.GetService<IMediator>()));

        public static Task<T> ExecuteDbContextAsync<T>(Func<RecipeManagerDbcontext, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<RecipeManagerDbcontext>()));

        public static Task<T> ExecuteDbContextAsync<T>(Func<RecipeManagerDbcontext, ValueTask<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<RecipeManagerDbcontext>()).AsTask());

        public static Task<T> ExecuteDbContextAsync<T>(Func<RecipeManagerDbcontext, IMediator, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<RecipeManagerDbcontext>(), sp.GetService<IMediator>()));

        public static Task<int> InsertAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<T>().Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }

        // MassTransit Methods -- Do Not Delete Comment

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            // MassTransit Teardown -- Do Not Delete Comment
        }
    }
}