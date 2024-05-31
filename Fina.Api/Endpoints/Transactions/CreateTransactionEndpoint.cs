using Fina.Api.Utils.Api;
using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/", HandleAsync)
             .WithName("Transactions: Create")
             .WithSummary("Cria uma nova transação")
             .WithDescription("Cria uma nova transação")
             .WithOrder(1)
             .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess ? TypedResults.Created($"v1/transactions/{response.Data?.Id}", response) : TypedResults.BadRequest(response);
        }
    }
}
