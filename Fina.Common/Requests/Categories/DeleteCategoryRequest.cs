using System.ComponentModel.DataAnnotations;

namespace Fina.Common.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        [Required]
        public long Id { get; set; }
    }
}
