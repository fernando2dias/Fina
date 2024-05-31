using Fina.Api.Utils.Api;
using Fina.Common;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as categorias")
            .WithDescription("Recupera todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ICategoryHandler handler,
                                                        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                        [FromQuery] int pageSize = Configuration.DetaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var response = await handler.GetAllAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
