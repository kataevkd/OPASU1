using Microsoft.AspNetCore.Mvc;
using RESTfull.Infrastructure;
using RESTfull.Infrastructure.Repository;
using RESTfull.Domain;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTfull.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly Context _context;
        private readonly publicationRepository _publicationRepository;
        public PublicationController(Context context)
        {
            _context = context;
            _publicationRepository = new publicationRepository(_context);
        }

        // GET: api/<PublicationContollerr>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPub()
        {
            var publication = await _publicationRepository.GetAllAsync();
            return publication.ToList();
        }

        // GET api/<PublicationContollerr>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPubId(Guid id)
        {
            var pub = await _publicationRepository.GetByIdAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            return pub;
        }

        // GET api/<AuthorController>/6

        [HttpGet("{PublicationName}")]
        public async Task<ActionResult<Publication>> GetPubName(string PublicationName)
        {
            var pub = await _publicationRepository.GetByNameAsync(PublicationName);
            if (pub == null)
            {
                return NotFound();
            }
            return pub;
        }

        /*
        // POST api/<PublicationContollerr>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PublicationContollerr>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PublicationContollerr>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */

    }
}
