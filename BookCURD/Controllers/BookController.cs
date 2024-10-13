using BookCURD.Data;
using BookCURD.Model;
using BookCURD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookCURD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly AuthService _authService;

        public BookController(BookContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // GET: api/book (Accessible by all, including unauthenticated users)
        [HttpGet]
        [AllowAnonymous]  // No authentication required
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // POST: api/book (Only Admins can add books)
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book, [FromHeader] string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !_authService.IsAdmin(user)) return Unauthorized(new { success = false, message = "Only Admins can add books." });

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, new { success = true, message = "Book added successfully!", book });
        }

        // PUT: api/book/{id} (Only Admins can update books)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book, [FromHeader] string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !_authService.IsAdmin(user)) return Unauthorized(new { success = false, message = "Only Admins can update books." });

            if (id != book.Id) return BadRequest(new { success = false, message = "Book ID mismatch." });

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Book updated successfully!" });
        }

        // DELETE: api/book/{id} (Only Admins can delete books)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, [FromHeader] string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !_authService.IsAdmin(user)) return Unauthorized(new { success = false, message = "Only Admins can delete books." });

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound(new { success = false, message = "Book not found." });

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Book deleted successfully!" });
        }
    }
}
