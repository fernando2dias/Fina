using Fina.Api.Utils.Api;
using Fina.Common;
using Fina.Common.Handlers;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/", HandleAsync)
             .WithName("Transactions: Get All")
             .WithSummary("Recupera transações por periodo")
             .WithDescription("Recupera transações por periodo")
             .WithOrder(5)
             .Produces<PagedResponse<List<Transaction>?>>();

        // se estiver recebendo um usuario logado usar o ClaimsPrincipal como parametro
        private static async Task<IResult> HandleAsync(ITransactionHandler handler,
                                                      [FromQuery] DateTime? startDate = null,
                                                      [FromQuery] DateTime? endDate = null,
                                                      [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                      [FromQuery] int pageSize = Configuration.DetaultPageSize
            )
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var response = await handler.GetByPeriodAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
        }
    }
}
