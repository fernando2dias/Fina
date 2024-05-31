using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response) : TypedResults.BadRequest(response);
        }
    }
}
