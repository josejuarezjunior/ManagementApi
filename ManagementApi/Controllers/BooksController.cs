using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using ManagementApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BooksController(IBookRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
            var books = _unitOfWork.BookRepository.GetAll().Where(x => x.IsDeleted == false);

            if (books is null)
                return NotFound();

            var produtosDto = _mapper.Map<IEnumerable<BookDTO>>(books);

            return Ok(produtosDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            var book = _unitOfWork.BookRepository.Get(x => x.Id == id);

            if (book is null)
                return NotFound("Book not found!");

            var bookDto = _mapper.Map<BookDTO>(book);

            return Ok(bookDto);
        }

        // IMPLEMENTAR!!!!
        //[HttpGet("deleted")]
        //public async Task<ActionResult<IEnumerable<Book>>> GetDeletedMovies()
        //{
        //    var deletedBooks = _repository.GetDeletedBooks();

        //    return deletedBooks is null ? NotFound() : Ok(deletedBooks);
        //}

        [HttpPost]
        public ActionResult<BookDTO> CreateBook(BookDTO bookDto)
        {
            if (bookDto is null)
                return BadRequest("Invalid book!");

            var book = _mapper.Map<Book>(bookDto);

            var newBook = _unitOfWork.BookRepository.Create(book);
            _unitOfWork.Commit();

            var newBookDto = _mapper.Map<BookDTO>(newBook);

            return Ok(newBookDto);
        }

        [HttpPut]
        public ActionResult<BookDTO> Put(int id, BookDTO bookDto)
        {
            if (id != bookDto.Id) 
                return BadRequest("Invalid book!");// Status Code 400

            var book = _mapper.Map<Book>(bookDto);

            var updatedBook = _unitOfWork.BookRepository.Update(book);
            _unitOfWork.Commit();

            var updatedBookDto = _mapper.Map<BookDTO>(updatedBook);

            return Ok(updatedBookDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<BookDTO> SoftDeleteBook(int id)
        {
            var book = _unitOfWork.BookRepository.Get(x => x.Id == id);

            if (id != book.Id)
                return NotFound("Book not found");

            var deletedBook = _unitOfWork.BookRepository.SoftDelete(id);
            _unitOfWork.Commit();

            var deletedBookDto = _mapper.Map<BookDTO>(deletedBook);

            return Ok(deletedBookDto);
        }
    }
}
