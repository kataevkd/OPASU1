using Microsoft.AspNetCore.Mvc;
using RESTfull.Infrastructure;
using RESTfull.Infrastructure.Repository;
using RESTfull.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTfull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly Context _context;
        private readonly AuthorRepository _authorRepository;
        public AuthorController(Context context)
        {
            _context = context;
            _authorRepository = new AuthorRepository(_context);
        }

        // GET: api/<AuthorController>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.ToList();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return author;
        }
        // GET api/<AuthorController>/6

        [HttpGet("{Name}")]
        public async Task<ActionResult<Author>> GetAuthorName(string name)
        {
            var author = await _authorRepository.GetByNameAsync(name);
            if (author == null)
            {
                return NotFound();
            }
            return author;
        }

        // GET api/<AuthorController>/7
        [HttpGet("{placeWork}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsByPlace(string placework)
        {
            var authors = await _authorRepository.GetByPlace(placework);
            return authors.ToList();
        }
        // POST api/<AuthorController>
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            var author2 = await _authorRepository.AddAsync(author);
            return CreatedAtAction("GetAuthor", new { id = author2.Id }, author2);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAuthor(Guid id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }


            await _authorRepository.UpdateAsync(author);

            return NoContent();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            await _authorRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool AuthorExists(Guid id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
