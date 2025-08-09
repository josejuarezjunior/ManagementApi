using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Movie> GetDeletedMovies()
        {
            return GetAll().Where(x => x.IsDeleted == true);
        }

    }
}
