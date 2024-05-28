﻿using System.ComponentModel.DataAnnotations;

namespace Fina.Common.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        [Required]
        public long Id { get; set; }

        [Required(ErrorMessage = "Título inválido")]
        [MaxLength(80, ErrorMessage = "O Título deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválida")]
        public string Description { get; set; } = string.Empty;
    }
}
