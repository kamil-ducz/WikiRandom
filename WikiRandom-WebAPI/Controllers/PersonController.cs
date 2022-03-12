using Microsoft.AspNetCore.Mvc;
using WikiRandom_WebAPI.Models;
using WikiRandom_WebAPI.Services;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PersonController : ControllerBase
    {
        private readonly IWikipediaService wikipediaService;

        public PersonController(IWikipediaService wikipediaService)
        {
            this.wikipediaService = wikipediaService;
        }
        [HttpGet]
        public ActionResult GetPersons()
        {
            var persons = wikipediaService.GetPeople();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public ActionResult GetPerson([FromRoute] int id)
        {
            var person = wikipediaService.GetPerson(id);

            return Ok(person);

        }

        [HttpPost]
        public ActionResult InsertPerson([FromBody] PersonDTO dto)
        {
            var id = wikipediaService.InsertPerson(dto);
            return Created($"persons/{id}", $"Person {dto.Name} created.");

        }

        [HttpDelete("{id}")]
        public ActionResult DeletePerson([FromRoute] int id)
        {
            wikipediaService.DeletePerson(id);
            return Accepted();

        }

        [HttpPut("{id}")]
        public ActionResult PutPerson([FromBody] PersonDTO personDTO, int id)
        {
            wikipediaService.PutPerson(personDTO, id);

            return Accepted($"person/{id}", $"Updated contact field for person with id={id}");

        }

        [HttpPatch("{id}")]
        public ActionResult PatchPerson([FromBody] PersonDTO personDTO, int id)
        {
            wikipediaService.PatchPerson(personDTO, id);

            return Ok();

        }

        //TODO users, authorization, authentication ; example: JWT authentication
        //TODO Swagger
        // js can get data from API
    }
}
