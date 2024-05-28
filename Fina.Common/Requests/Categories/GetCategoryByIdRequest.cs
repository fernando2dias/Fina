using System.ComponentModel.DataAnnotations;

namespace Fina.Common.Requests.Categories
{
    public class GetCategoryByIdRequest : Request
    {
        [Required]
        public long Id { get; set; }
    }
}
