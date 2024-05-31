using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/{id}", HandleAsync)
             .WithName("Transactions: Get By Id")
             .WithSummary("Recupera transação por Id")
             .WithDescription("Recupera transação por Id")
             .WithOrder(4)
             .Produces<Response<Transaction?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
