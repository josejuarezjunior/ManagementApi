using Core.Entities;

namespace Core.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> GetDeletedMovies();
    }
}
