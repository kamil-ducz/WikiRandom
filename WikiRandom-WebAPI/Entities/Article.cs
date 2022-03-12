namespace WikiRandom_WebAPI.Entities
{
    /// <summary>
    /// Random Wikipedia title entity
    /// </summary>
    public class Article
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Lenght { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
