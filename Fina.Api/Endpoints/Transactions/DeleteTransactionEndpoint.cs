using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Exclui uma transação")
            .WithDescription("Exclui uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await handler.DeleteAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
