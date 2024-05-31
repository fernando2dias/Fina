using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var response = await handler.UpdateAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
