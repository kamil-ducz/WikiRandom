using System.ComponentModel.DataAnnotations;
using WikiRandom_WebAPI.Entities;

namespace WikiRandom_WebAPI.Models
{
    public class PersonDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public List<ArticleDTO>? Articles { get; set; }
    }
}
