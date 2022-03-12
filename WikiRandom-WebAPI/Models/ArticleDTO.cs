using System.ComponentModel.DataAnnotations;
using WikiRandom_WebAPI.Entities;

namespace WikiRandom_WebAPI.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Content { get; set; }
        public int Lenght { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? PersonId { get; set; }
    }
}
