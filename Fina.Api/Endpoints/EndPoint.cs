using Fina.Api.Endpoints.Categories;
using Fina.Api.Endpoints.Transactions;
using Fina.Api.Utils.Api;

namespace Fina.Api.Endpoints
{
    public static class EndPoint
    {
        public static void MapEndPoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            //.RequireAuthorization()
            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            ////.RequireAuthorization()
            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionByPeriodEndpoint>();

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
