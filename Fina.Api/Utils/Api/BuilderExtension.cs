using Fina.Api.Handlers;
using Fina.Common;
using Fina.Common.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Utils.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString  = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;

        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(y => y.FullName));
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => options.AddPolicy(
                    ApiConfiguration.CorsPoliceName,
                    policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    ])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                ));
        }

        public static void AddDataContexts(this WebApplicationBuilder builder) {
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(ApiConfiguration.ConnectionString));
        }


        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
        }

    }
}
