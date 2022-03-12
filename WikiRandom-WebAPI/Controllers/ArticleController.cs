using Microsoft.AspNetCore.Mvc;
using WikiRandom_WebAPI.Models;
using WikiRandom_WebAPI.Services.Classes;

namespace WikiRandom_WebAPI.Controllers
{
    //Article is subentity to Person
    [Route("person/{personId}/article")]
    [ApiController]

    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly WikipediaService wikipediaService;

        public ArticleController(ILogger<ArticleController> logger, WikipediaService wikipediaService)
        {
            _logger = logger;
            this.wikipediaService = wikipediaService;
        }

        [HttpGet]
        public ActionResult GetArticlesDb([FromRoute]int personId)
        {
            var articlesDb = wikipediaService.GetArticlesFromDb(personId);

            return Ok(articlesDb);
        }

        [HttpGet]
        [Route("download-one")]
        public ActionResult GetArticle([FromRoute] int personId)
        {
            var article = wikipediaService.GetSingleArticle(personId);

            return Ok(article.Result);
        }

        [HttpGet]
        [Route("download-many")]
        public async Task<ActionResult> GetArticles([FromRoute]int personId, int count)
        {
            return Ok(await wikipediaService.GetArticles(personId, count));

        }

        [HttpGet]
        [Route("download-many/fav")]
        public async Task<ActionResult> GetArticles([FromRoute] int personId,
                                                    int count, int artLenght)
        {
            if (count <= 0)
            {
                return BadRequest("Number of articles to return has to be a positive integer value.");
            }
            var articles = await wikipediaService.GetArticles(personId, count, artLenght);
            if (articles is null)
            {
                return NotFound("No article has been found. Check your internet connection and retry.");
            }

            return Ok(await wikipediaService.GetArticles(personId, count, artLenght));
        }

        [HttpPost]
        public async Task<ActionResult> PostArticle([FromRoute]int personId)
        {
            var articleTitle = await wikipediaService.GetSingleArticle(personId);
            wikipediaService.InsertArticle(personId, articleTitle);
            return Ok($"Created article {articleTitle} and assigned to person with id={personId}.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArticle([FromRoute]int personId, [FromRoute] int id)
        {

            wikipediaService.DeleteArticle(id);
            return Accepted();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticle([FromRoute]int personId, [FromBody] ArticleDTO articleDTO, int id)
        {
            wikipediaService.PutArticle(personId, articleDTO, id);
            return Accepted();

        }

        [HttpPatch("{id}")]
        public ActionResult PatchArticle([FromRoute]int personId, [FromBody] ArticleDTO articleDTO, int id)
        {
            wikipediaService.PatchArticle(personId, articleDTO, id);
            return Accepted($"Updated {articleDTO.Content}");

        }
    }
}
