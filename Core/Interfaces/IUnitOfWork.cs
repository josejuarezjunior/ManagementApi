namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }
        IBookRepository BookRepository { get; }
        void Commit();
    }
}
