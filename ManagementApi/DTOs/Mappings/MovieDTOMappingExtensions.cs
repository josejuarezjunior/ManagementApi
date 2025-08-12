using Core.Entities;

namespace ManagementApi.DTOs.Mappings
{
    public static class MovieDTOMappingExtensions
    {
        public static MovieDTO? ToMovieDTO(this Movie movie)
        {
            if (movie is null)
                return null;

            return new MovieDTO
            {
                Id = movie.Id,
                IsDeleted = movie.IsDeleted,
                Title = movie.Title,
                Year = movie.Year,
                Director = movie.Director,
            };
        }

        public static Movie? ToMovie(this MovieDTO movieDto)
        {
            if (movieDto is null)
                return null;

            return new Movie
            {
                Id = movieDto.Id,
                IsDeleted = movieDto.IsDeleted,
                Title = movieDto.Title,
                Year = movieDto.Year,
                Director = movieDto.Director,
            };
        }

        public static IEnumerable<MovieDTO> ToMovieDTOList(this IEnumerable<Movie> movies)
        {
            if (movies is null || !movies.Any())
                return new List<MovieDTO>();

            //var moviesDtoList = new List<MovieDTO>();

            //foreach (var movie in movies) 
            //{
            //    var movieDto = new MovieDTO
            //    {
            //        Id = movie.Id,
            //        IsDeleted = movie.IsDeleted,
            //        Title = movie.Title,
            //        Year = movie.Year,
            //        Director = movie.Director,
            //    };

            //    moviesDtoList.Add(movieDto);
            //}

            //return moviesDtoList;

            return movies.Select(movie => new MovieDTO
            {
                Id = movie.Id,
                IsDeleted = movie.IsDeleted,
                Title = movie.Title,
                Year = movie.Year,
                Director = movie.Director,
            }).ToList();
        }
    }
}
