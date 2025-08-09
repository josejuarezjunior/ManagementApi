using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using ManagementApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IMovieRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDTO>> GetMovies()
        {
            var movies = _unitOfWork.MovieRepository.GetAll().Where(x => x.IsDeleted == false);

            if (movies is null)
                return NotFound();

            var moviesDto = new List<MovieDTO>();

            foreach (var movie in movies)
            {
                var movieDto = new MovieDTO
                {
                    Id = movie.Id,
                    IsDeleted = movie.IsDeleted,
                    Title = movie.Title,
                    Year = movie.Year,
                    Director = movie.Director,
                };
                moviesDto.Add(movieDto);
            }

            return Ok(moviesDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<MovieDTO> GetMovie(int id)
        {
            //var movie = _repository.Get(x => x.Id == id);
            var movie = _unitOfWork.MovieRepository.Get(x => x.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDto = new MovieDTO()
            {
                Id = movie.Id,
                IsDeleted = movie.IsDeleted,
                Title = movie.Title,
                Year = movie.Year,
                Director = movie.Director,
            };

            return Ok(movieDto);
        }

        [HttpGet("deleted")]
        public ActionResult<IEnumerable<MovieDTO>> GetDeletedMovies()
        {
            var deletedMovies = _unitOfWork.MovieRepository.GetDeletedMovies();
            
            if (deletedMovies is null)
                return NotFound();

            var deletedMoviesDto = new List<MovieDTO>();

            foreach (var deletedMovie in deletedMovies)
            {
                var deletedMovieDto = new MovieDTO
                {
                    Id = deletedMovie.Id,
                    IsDeleted = deletedMovie.IsDeleted,
                    Title = deletedMovie.Title,
                    Year = deletedMovie.Year,
                    Director = deletedMovie.Director,
                };
                deletedMoviesDto.Add(deletedMovieDto);
            }

            return Ok(deletedMoviesDto);
        }

        [HttpPost]
        public ActionResult<MovieDTO> CreateMovie(MovieDTO movieDto)
        {
            if (movieDto is null)
                return BadRequest();

            var movie = new Movie()
            {
                Id = movieDto.Id,
                IsDeleted = movieDto.IsDeleted,
                Title = movieDto.Title,
                Year = movieDto.Year,
                Director = movieDto.Director,
            };

            var createdMovie = _unitOfWork.MovieRepository.Create(movie);
            _unitOfWork.Commit();

            var createdMovieDto = new MovieDTO()
            {
                Id = createdMovie.Id,
                IsDeleted = createdMovie.IsDeleted,
                Title = createdMovie.Title,
                Year = createdMovie.Year,
                Director = createdMovie.Director,
            };

            return Ok(createdMovieDto);
        }

        [HttpPut]
        public ActionResult<MovieDTO> Put(int id, MovieDTO movieDto)
        {
            if (id != movieDto.Id) 
                return BadRequest("Invalid movie!"); // Status Code 400

            var movie = new Movie()
            {
                Id = movieDto.Id,
                IsDeleted = movieDto.IsDeleted,
                Title = movieDto.Title,
                Year = movieDto.Year,
                Director = movieDto.Director,
            };

            var updatedMovie = _unitOfWork.MovieRepository.Update(movie);
            _unitOfWork.Commit();

            var updatedMovieDto = new MovieDTO()
            {
                Id = updatedMovie.Id,
                IsDeleted = updatedMovie.IsDeleted,
                Title = updatedMovie.Title,
                Year = updatedMovie.Year,
                Director = updatedMovie.Director,
            };

            return Ok(updatedMovieDto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult<MovieDTO> SoftDeleteMovie(int id)
        {
            //var movie = _repository.Get(x => x.Id == id);
            var movie = _unitOfWork.MovieRepository.Get(x => x.Id == id);

            if (movie is null) 
                return NotFound("Movie not found.");

            //var deletedMovie = _repository.SoftDelete(id);
            var deletedMovie = _unitOfWork.MovieRepository.SoftDelete(id);
            _unitOfWork.Commit();

            var deletedMovieDto = new MovieDTO()
            {
                Id = deletedMovie.Id,
                IsDeleted = deletedMovie.IsDeleted,
                Title = deletedMovie.Title,
                Year = deletedMovie.Year,
                Director = deletedMovie.Director,
            };

            return Ok(deletedMovieDto);
        }
    }
}
