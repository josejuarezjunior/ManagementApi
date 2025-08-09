using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        //private readonly IMovieRepository _repository;// Herda todos os métodos do repositório genérico.
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IMovieRepository repository, IUnitOfWork unitOfWork)
        {
            //_repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            //var movies = _repository.GetAll().Where(x => x.IsDeleted == false);
            var movies = _unitOfWork.MovieRepository.GetAll().Where(x => x.IsDeleted == false);

            return movies is null ? NotFound() : Ok(movies);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            //var movie = _repository.Get(x => x.Id == id);
            var movie = _unitOfWork.MovieRepository.Get(x => x.Id == id);

            return movie is null ? NotFound() : Ok(movie);
        }

        [HttpGet("deleted")]
        public ActionResult<IEnumerable<Movie>> GetDeletedMovies()
        {
            //var deletedMovies = _repository.GetDeletedMovies();
            var deletedMovies = _unitOfWork.MovieRepository.GetDeletedMovies();

            return deletedMovies is null ? NotFound() : Ok(deletedMovies);
        }

        [HttpPost]
        public ActionResult<Movie> CreateMovie(Movie movie)
        {
            if (movie is null)
                return BadRequest();

            //var createdMovie = _repository.Create(movie);
            var createdMovie = _unitOfWork.MovieRepository.Create(movie);
            _unitOfWork.Commit();
            return Ok(createdMovie);
        }

        [HttpPut]
        public ActionResult Put(int id, Movie movie)
        {
            if (id != movie.Id) 
                return BadRequest("Invalid movie!"); // Status Code 400

            //var updatedMovie = _repository.Update(movie);
            var updatedMovie = _unitOfWork.MovieRepository.Update(movie);
            _unitOfWork.Commit();
            return Ok(updatedMovie);

        }

        [HttpDelete("{id:int}")]
        public ActionResult<Movie> SoftDeleteMovie(int id)
        {
            //var movie = _repository.Get(x => x.Id == id);
            var movie = _unitOfWork.MovieRepository.Get(x => x.Id == id);

            if (movie is null) 
                return NotFound("Movie not found.");

            //var deletedMovie = _repository.SoftDelete(id);
            var deletedMovie = _unitOfWork.MovieRepository.SoftDelete(id);
            _unitOfWork.Commit();
            return Ok(deletedMovie);
        }
    }
}
