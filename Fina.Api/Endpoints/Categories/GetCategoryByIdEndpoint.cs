using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Recupera categoria por Id")
            .WithDescription("Recupera categoria por Id")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
