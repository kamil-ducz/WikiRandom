using System.ComponentModel.DataAnnotations;

namespace WikiRandom_WebAPI.Entities
{
    /// <summary>
    /// Article owners entity
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public List<Article>? Articles { get; set; }

    }
}
