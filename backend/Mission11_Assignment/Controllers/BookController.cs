using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mission11_Assignment.Data;

namespace WaterProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private BookDbContext _bookContext;

        public BookController(BookDbContext temp)
        {
            _bookContext = temp;
        }

        [HttpGet("AllBooks")]
        public IActionResult GetBooks(int pageSize = 10, int pageNum = 1, string sortOrder = "asc")
        {
            var query = _bookContext.Books.AsQueryable();

            // Apply sorting based on title
            if (sortOrder.ToLower() == "asc")
            {
                query = query.OrderBy(b => b.Title);
            }
            else if (sortOrder.ToLower() == "desc")
            {
                query = query.OrderByDescending(b => b.Title);
            }

            var books = query
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalNumBooks = _bookContext.Books.Count();

            var response = new
            {
                Books = books,
                TotalNumBooks = totalNumBooks
            };

            return Ok(response);
        }




    }
}
