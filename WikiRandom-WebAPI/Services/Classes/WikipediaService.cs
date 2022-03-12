﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Middleware;
using WebApplication4.Services;
using WikiRandom_WebAPI.Entities;
using WikiRandom_WebAPI.Models;
using WikiRandom_WebAPI.Services.Interfaces;
using static Services.ConstDefinitions;

namespace WikiRandom_WebAPI.Services.Classes
{
    public class WikipediaService : IWikipediaService
    {
        private readonly IWikiService wikiService;
        private readonly WikiRandomDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<WikipediaService> logger;

        public WikipediaService(IWikiService wikiService, WikiRandomDbContext dbContext,
            IMapper mapper, ILogger<WikipediaService> logger)
        {
            this.wikiService = wikiService;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        //helper methods
        private async Task<List<string>> LoadArticles(int count)
        {
            List<string> articles = new List<string>();

            for (int i = 0; i < count; i++)
            {
                articles.Add(await this.wikiService.GetWikiRandom());
            }

            return articles;

        }

        private int GetRandomPersonId()
        {
            Random random = new Random();
            int toSkip = random.Next(0, dbContext.Persons.Count());
            var randomPerson = dbContext.Persons.Skip(toSkip).Take(1).First();
            return randomPerson.Id;

        }
        /// <summary>
        /// Check if articles has assigned person
        /// </summary>
        /// <param name="id">PersonId</param>
        /// <returns></returns>
        private bool HasAssignedPerson(int id)
        {
            return dbContext.Articles.Any(a => a.PersonId == id);
        }

        private void UpdatePersonArticle(HttpRESTMethods httpMethod, PersonDTO personDTO, Person person,
                                List<ArticleDTO> articlesJSON, List<Article> articlesDB)
        {
            foreach (var articleJSON in articlesJSON)
            {
                foreach (var articleDB in articlesDB)
                {
                    if (articleJSON.Id == articleDB.Id)
                    {                        
                        articleDB.Content = articleJSON.Content;
                        articleDB.Lenght = articleJSON.Lenght;
                        articleDB.CreatedAt = articleJSON.CreatedAt;
                        articleDB.PersonId = articleJSON.PersonId;
                        if (httpMethod == HttpRESTMethods.POST && articleJSON.PersonId is null)
                        {
                            articleDB.PersonId = personDTO.Id;
                        }
                        else if (httpMethod == HttpRESTMethods.PUT && articleJSON.PersonId is null)
                        {
                            throw new BadRequestException("Article with proper id and matching PersonId" +
                              " has to be provided");
                        }
                        else if (articleJSON.PersonId != person.Id)
                        {
                            throw new BadRequestException("Article with proper id and matching PersonId" +
                          " has to be provided");
                        }
                        //break;
                    }

                }
            }
        }

        //methods dedicated to REST
        //persons
        public IEnumerable<PersonDTO> GetPeople()
        {
            var persons = dbContext.Persons
                .Include(p => p.Articles)
                .ToList();
            return mapper.Map<IEnumerable<PersonDTO>>(persons);

        }

        public PersonDTO GetPerson(int id)
        {
            var person = dbContext.Persons
                .Include(a => a.Articles)
                .FirstOrDefault(p => p.Id == id);

            if (person is null)
            {
                throw new NotFoundException("Record not found");
            }

            return mapper.Map<PersonDTO>(person);

        }

        public int InsertPerson(PersonDTO personDTO)
        {
            var dto = mapper.Map<Person>(personDTO);
            dbContext.Persons.Add(dto);
            dbContext.SaveChanges();
            return dto.Id;

        }

        public void DeletePerson(int id)
        {
            var personToDelete = dbContext.Persons
                                          .FirstOrDefault(p => p.Id == id);
            if (personToDelete is null)
            {
                throw new NotFoundException("Record not found");
            }

            var potentialArticle = HasAssignedPerson(id);

            if (potentialArticle)  //transaction required
            {
                throw new BadRequestException($"Cannot delete person with id={personToDelete.Id} " +
                                  $"as this person has at least one article.");
            }

            dbContext.Persons.Remove(personToDelete);
            dbContext.SaveChanges();

        }
        /*
            1. ArticleJSON id not provided or empty array --> clean articlesDB
            2. articleDB id == ArticleJSON id --> update article in DB
            3. articleDB id != ArticleJSON id -> Bad request
         */
        //TODO orphaned articles are left when removal takes place
        public void PutPerson(PersonDTO personDTO, int id)
        {
            var person = dbContext
                .Persons
                .Include(p => p.Articles)
                .FirstOrDefault(p => p.Id == id)
                ;
            if (person == null)
            {
                throw new NotFoundException("Record not found");
            }
            person.Name = personDTO.Name;
            person.Contact = personDTO.Contact;

            var articlesJSON = personDTO.Articles;
            var articlesDB = person.Articles;


            foreach (var articleDB in articlesDB)
            {
                if (!articlesJSON.Any(y => y.Id == articleDB.Id))
                {
                    dbContext.Articles.Remove(articleDB);
                }
            }
            articlesDB.RemoveAll(x => !articlesJSON.Any(y => y.Id == x.Id));
            //case 1
            if (articlesJSON is null)
            {
                articlesDB.RemoveRange(0, articlesDB.Count);
            }
            //case 3
            else if (articlesJSON.Any(x => !articlesDB.Any(y => y.Id == x.Id)))
            {
                throw new BadRequestException("Bad request");
            }
            //case 2
            else if (articlesDB is not null)
            {
                UpdatePersonArticle(HttpRESTMethods.POST, personDTO, person, articlesJSON, articlesDB);

            }
            dbContext.SaveChanges();

        }
        /*
            1. articleJSON id not provided --> leave article ; empty array --> clear articlesDB
            2. articleDB id == articleJSON id --> update article in DB
            3. articleDB id != articleJSON id -> Bad request
         */
        public void PatchPerson(PersonDTO personDTO, int id)
        {
            var person = dbContext
                        .Persons
                        .Include(p => p.Articles)
                        .FirstOrDefault(p => p.Id == id)
    ;
            if (person == null)
            {
                throw new NotFoundException("Record not found");
            }

            if (personDTO.Name is not null)
            {
                person.Name = personDTO.Name;
            }
            if (personDTO.Contact is not null)
            {
                person.Contact = personDTO.Contact;
            }

            var articlesJSON = personDTO.Articles;
            var articlesDB = person.Articles;

            //case 1
            if (articlesJSON is not null && articlesJSON.Count == 0)
            {
                articlesDB.RemoveRange(0, articlesDB.Count);
            }

            //case 3
            if (articlesJSON is not null && articlesJSON.Any(x => x.Id != 0 && !articlesDB.Any(y => y.Id == x.Id)))
            {
                throw new BadRequestException("Article with proper id has to be provided");
            }
            //case 2
            if (articlesJSON is not null && articlesDB is not null)
            {
                UpdatePersonArticle(HttpRESTMethods.PUT, personDTO, person, articlesJSON, articlesDB);
            }
            dbContext.SaveChanges();
        }

        //articles
        public List<ArticleDTO> GetArticlesFromDb()
        {
            var articles = dbContext.Articles
                .Include(p => p.Person)
                .ToList();
            var articlesDTO = mapper.Map<List<ArticleDTO>>(articles);
            return articlesDTO;

        }

        public async Task<string> GetSingleArticle()
        {
            return await this.wikiService.GetWikiRandom();

        }
        /// <summary>
        /// Retrieve article with provied id from the database
        /// </summary>
        /// <param name="id">Required article id</param>
        /// <returns></returns>
        public Article GetArticleFromDb(int id)
        {
            return dbContext.Articles.FirstOrDefault(a => a.Id == id);
        }

        public async Task<List<string>> GetArticles(int count)
        {
            return await LoadArticles(count);
        }

        public async Task<List<string>> GetArticles(int count, int artLenght)
        {
            var articles = await LoadArticles(count);
            return articles.Where(a => a.Length < artLenght).ToList();
        }

        public void InsertArticle(string articleHeader)
        {
            Article articleToInsert = new Article();
            articleToInsert.Content = articleHeader;
            articleToInsert.Lenght = articleHeader.Length;
            articleToInsert.CreatedAt = DateTime.Now;
            articleToInsert.PersonId = GetRandomPersonId();
            dbContext.Articles.Add(articleToInsert);
            dbContext.SaveChanges();
        }

        public void DeleteArticle(int id)
        {
            dbContext.Articles.Remove(GetArticleFromDb(id));
            dbContext.SaveChanges();

        }

        public void PutArticle(ArticleDTO articleDTO, int id)
        {
            var article = GetArticleFromDb(id);

            if (article is null)
            {
                throw new NotFoundException("Record not found");
            }

            article.Content = articleDTO.Content;
            article.Lenght = articleDTO.Lenght;
            article.CreatedAt = articleDTO.CreatedAt;
            article.PersonId = articleDTO.PersonId;
            dbContext.Articles.Update(article);
            dbContext.SaveChanges();

        }

        public void PatchArticle(ArticleDTO articleDTO, int id)
        {
            var article = GetArticleFromDb(id);
            var datetime = new DateTime(0001, 01, 01);

            if (article is null)
            {
                throw new NotFoundException("Record not found");
            }

            if (articleDTO.Content is not null)
            {
                article.Content = articleDTO.Content;
            }

            if (articleDTO.Lenght != 0)
            {
                article.Lenght = articleDTO.Lenght;
            }

            if (articleDTO.CreatedAt != datetime)
            {
                article.CreatedAt = articleDTO.CreatedAt;
            }

            if (articleDTO.PersonId is not null)
            {
                article.PersonId = articleDTO.PersonId;

            }

            dbContext.Articles.Update(article);
            dbContext.SaveChanges();

        }

    }
}
