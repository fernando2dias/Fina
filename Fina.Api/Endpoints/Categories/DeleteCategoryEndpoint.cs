using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Exclui uma categoria")
            .WithDescription("Exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };
                  
            var response = await handler.DeleteAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
