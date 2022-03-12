using Microsoft.AspNetCore.Mvc;
using WikiRandom_WebAPI.Entities;
using WikiRandom_WebAPI.Models;
using WikiRandom_WebAPI.Services.Interfaces;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WikiServiceController : ControllerBase
    {
        private readonly ILogger<WikiServiceController> _logger;
        private readonly IWikipediaService wikipediaService;

        public WikiServiceController(ILogger<WikiServiceController> logger, IWikipediaService wikipediaService)
        {
            _logger = logger;
            this.wikipediaService = wikipediaService;
        }
        [HttpGet("persons")]
        public ActionResult GetPersons([FromRoute] int id)
        {
            var persons = wikipediaService.GetPeople();

            return Ok(persons);
        }

        [HttpGet("persons/{id}")]
        public ActionResult GetPerson([FromRoute] int id)
        {
            var person = wikipediaService.GetPerson(id);

            return Ok(person);

        }

        [HttpPost]
        [Route("persons")]
        public ActionResult InsertPerson([FromBody] PersonDTO dto)
        {
            var id = wikipediaService.InsertPerson(dto);
            return Created($"persons/{id}", $"Person {dto.Name} created.");

        }

        [HttpDelete("persons/{id}")]
        public ActionResult DeletePerson([FromRoute] int id)
        {
            wikipediaService.DeletePerson(id);
            return Accepted();

        }

        [HttpPut("persons/{id}")]
        public ActionResult PutPerson([FromBody] PersonDTO personDTO, int id)
        {
            wikipediaService.PutPerson(personDTO, id);

            return Accepted($"persons/{id}", $"Updated contact field for person with id={id}");

        }

        [HttpPatch("persons/{id}")]
        public ActionResult PatchPerson([FromBody] PersonDTO personDTO, int id)
        {
            wikipediaService.PatchPerson(personDTO, id);

            return Ok();

        }

        //articles
        [HttpGet("articlesdb")]
        public ActionResult GetArticlesDb()
        {
            var articlesDb = wikipediaService.GetArticlesFromDb();
            if (articlesDb is null)
            {
                return NotFound("There are no records to display");
            }
            return Ok(articlesDb);
        }

        [HttpGet]
        public ActionResult GetArticle()
        {
            var article = wikipediaService.GetSingleArticle();
            if (article is null)
            {
                NotFound("No article has been found. Check your internet connection and retry.");
            }
            return Ok(article.Result);
        }

        [HttpGet]
        [Route("articles")]
        public async Task<ActionResult> GetArticles(int count)
        {
            if (count <= 0)
            {
                return BadRequest("Number of articles to return has to be a positive integer value.");
            }
            var articles = await wikipediaService.GetArticles(count);
            if (articles is null)
            {
                return NotFound("No article has been found. Check your internet connection and retry.");
            }
            _logger.LogInformation("Method which returns certain number of articles invoked! Number of articles passed: {0}", count);

            return Ok(articles);
        }

        [HttpGet]
        [Route("articles/fav")]
        public async Task<ActionResult> GetArticles(int count, int artLenght)
        {
            if (count <= 0)
            {
                return BadRequest("Number of articles to return has to be a positive integer value.");
            }
            var articles = await wikipediaService.GetArticles(count, artLenght);
            if (articles is null)
            {
                return NotFound("No article has been found. Check your internet connection and retry.");
            }
            _logger.LogInformation("Method which returns certain number of articles and their max lenght invoked! Number of articles passed: {0}", count);

            return Ok(articles);
        }

        [HttpPost("articles")]
        public async Task<ActionResult> PostArticle()
        {
            var articleTitle = await wikipediaService.GetSingleArticle();
            wikipediaService.InsertArticle(articleTitle);
            return Ok(articleTitle);
        }

        [HttpDelete("articles/{id}")]
        public ActionResult DeleteArticle([FromRoute] int id)
        {
            var articleToDelete = wikipediaService.GetArticleFromDb(id);
            if (articleToDelete is null)
            {
                return NotFound("Either article with provided id does not exist or provided id was invalid.");
            }
            wikipediaService.DeleteArticle(articleToDelete.Id);
            return Accepted();
        }

        [HttpPut("articles/{id}")]
        public async Task<ActionResult> UpdateArticle([FromBody] ArticleDTO articleDTO, int id)
        {

            wikipediaService.PutArticle(articleDTO, id);

            return Accepted();
        }

        [HttpPatch("articles/{id}")]
        public ActionResult PatchArticle([FromBody] ArticleDTO articleDTO, int id)
        {
            wikipediaService.PatchArticle(articleDTO, id);
            return Accepted($"Updated {articleDTO.Content}");

        }

        //TODO users, authorization, authentication ; example: JWT authentication
    }
}
