using Fina.Common.Handlers;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria criada com sucesso!");
            }
            catch (Exception e)
            {
                //TODO: Seriglog, OpenTelemetry
                return new Response<Category?>(null, 500, $"Não foi possível criar uma categoria: {e.Message}");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria atualzada com sucesso!");
            }
            catch (Exception e)
            {
                //TODO: Seriglog, OpenTelemetry
                return new Response<Category?>(null, 500, $"Não foi possível remover a categoria: {e.Message}");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query =  context.Categories
                    .AsNoTracking()
                    .Where(c => c.UserId == request.UserId)
                    .OrderBy(c => c.Title);

                var categories = await query
                    .Skip((request.PageNumber-1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);

            }
            catch (Exception e)
            {
                //TODO: Seriglog, OpenTelemetry
                return new PagedResponse<List<Category>?>(null, 500, "Não foi possível obter a lista");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return category is null ? new Response<Category?>(null, 404, "Categoria não encontrada!") : new Response<Category?>(category, 200);

            }
            catch (Exception e)
            {
                //TODO: Seriglog, OpenTelemetry
                return new Response<Category?>(null, 500, $"Não foi possível encontrar a categoria: {e.Message}");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria atualzada com sucesso!");
            }
            catch (Exception e)
            {
                //TODO: Seriglog, OpenTelemetry
                return new Response<Category?>(null, 500, $"Não foi possível atualizar a categoria: {e.Message}");
            }
        }
    }
}
