using AutoMapper;
using WikiRandom_WebAPI.Entities;
using WikiRandom_WebAPI.Models;

namespace WikiRandom_WebAPI
{
    public class WikipediaServiceMappingProfile : Profile
    {
        public WikipediaServiceMappingProfile()
        {
            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();
        }
    }
}
