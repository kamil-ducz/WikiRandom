using WikiRandom_WebAPI.Entities;
using WikiRandom_WebAPI.Models;

namespace WikiRandom_WebAPI.Services.Interfaces
{
    /// <summary>
    /// Has methods dedicated to REST, works on a database
    /// </summary>
    public interface IWikipediaService
    {
        //persons
        IEnumerable<PersonDTO> GetPeople();
        PersonDTO GetPerson(int id);
        int InsertPerson(PersonDTO person);
        void DeletePerson(int id);
        void PutPerson(PersonDTO personDTO, int id);
        void PatchPerson(PersonDTO personDTO, int id);
        //articles
        List<ArticleDTO> GetArticlesFromDb();
        Article GetArticleFromDb(int id);
        Task<string> GetSingleArticle();
        Task<List<string>> GetArticles(int count);
        Task<List<string>> GetArticles(int count, int artLenght);
        void InsertArticle(string article);
        void DeleteArticle(int id);
        void PutArticle(ArticleDTO articleDTO, int id);
        void PatchArticle(ArticleDTO articleDTO, int id);
    }
}
