using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = _unitOfWork.BookRepository.GetAll().Where(x => x.IsDeleted == false);

            return books is null ? NotFound() : Ok(books);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _unitOfWork.BookRepository.Get(x => x.Id == id);

            return book is null ? NotFound("Book not found!") : Ok(book);
        }

        // IMPLEMENTAR!!!!
        //[HttpGet("deleted")]
        //public async Task<ActionResult<IEnumerable<Book>>> GetDeletedMovies()
        //{
        //    var deletedBooks = _repository.GetDeletedBooks();

        //    return deletedBooks is null ? NotFound() : Ok(deletedBooks);
        //}

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            if (book is null)
                return BadRequest("Invalid book!");

            var newBook = _unitOfWork.BookRepository.Create(book);
            _unitOfWork.Commit();

            return Ok(newBook);
        }

        [HttpPut]
        public ActionResult Put(int id, Book book)
        {
            if (id != book.Id) 
                return BadRequest("Invalid book!");// Status Code 400

            var updatedBook = _unitOfWork.BookRepository.Update(book);
            _unitOfWork.Commit();

            return Ok(updatedBook);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Book> SoftDeleteBook(int id)
        {
            var book = _unitOfWork.BookRepository.Get(x => x.Id == id);

            if (id != book.Id)
                return NotFound("Book not found");

            var deletedBook = _unitOfWork.BookRepository.SoftDelete(id);
            _unitOfWork.Commit();

            return Ok(deletedBook);
        }
    }
}
