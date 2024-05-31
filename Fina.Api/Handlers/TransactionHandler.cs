using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Fina.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: Common.Enums.ETransactionType.Withdraw } && request.Amount > 0)
            {
                request.Amount *= -1;
            }

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            }
            catch (Exception e)
            {
                return new Response<Transaction?>(null, 500, $"Não foi possível efetuar a transação:{e.Message}");
            }

        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação removida com sucesso!");
            }
            catch (Exception e)
            {
                return new Response<Transaction?>(null, 500, $"Não foi possível remover a transação:{e.Message}");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }

                return new Response<Transaction?>(transaction, 200);
            }
            catch (Exception e)
            {
                return new Response<Transaction?>(null, 500, $"Não foi possível encontrar a transação:{e.Message}");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();

                var query = context.Transactions
                    .AsNoTracking()
                    .Where(t => t.PaidOrReceivedAt >= request.StartDate &&
                                t.PaidOrReceivedAt <= request.EndDate &&
                                t.UserId == request.UserId)
                          .OrderBy(t => t.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);

            }
            catch (Exception e)
            {
                return new PagedResponse<List<Transaction>?>(null, 500, $"Não foi possível encontrar a transação:{e.Message}");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: Common.Enums.ETransactionType.Withdraw } && request.Amount > 0)
            {
                request.Amount *= -1;
            }

            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso!");
            }
            catch (Exception e)
            {
                return new Response<Transaction?>(null, 500, $"Não foi possível atualizar a transação:{e.Message}");
            }
        }
    }
}
