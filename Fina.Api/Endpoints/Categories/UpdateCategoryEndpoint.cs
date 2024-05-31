using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;
           
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
