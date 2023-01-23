using Microsoft.AspNetCore.Mvc;
using RestApi_Template.Models;
using RestApi_Template.Services;

namespace RestApi_Template.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        
        public AuthorController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _libraryService.GetAuthorsAsync();
            if(authors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return StatusCode(StatusCodes.Status200OK, authors);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAuthor(Guid id, bool includedBooks = false)
        {
            Author author = await _libraryService.GetAuthorAsync(id, includedBooks);
            if(author == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
            }
            return StatusCode(StatusCodes.Status200OK, author);
        }


        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            var dbAuthor = await _libraryService.AddAuthorAsync(author);
            if (dbAuthor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Name} could not be added.");
            }
            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAuthor(Guid id,Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            Author dbAuthor = await _libraryService.UpdateAuthorAsync(author);
            if(author == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Name} could not be updated");
            }
            
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await _libraryService.GetAuthorAsync(id, false);
            (bool status, string message) = await _libraryService.DeleteAuthorAsync(author);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,message );
            }

            return StatusCode(StatusCodes.Status200OK, author);
        }
    }
}
