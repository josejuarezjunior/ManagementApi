using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWOrk : IUnitOfWork
    {
        // O padrão Unit of Work é a criação de uma classe responsável por gerenciar transações,
        // e mais importante, por chamar o Save Changes.
        // Essa classe é quem inicia, confirma ou reverte uma transação e garante que todas as
        // operações realizadas nos repositórios sejam incluídas em uma única transação do banco
        // de dados.
        // Na prática, está sendo usada apenas uma instância do contexto em toda a aplicação.
        // Dessa forma, não há problemas de concorrência.

        private IMovieRepository? _movieRepository;

        private IBookRepository? _bookRepository;

        public IMovieRepository MovieRepository
        {
            // Usar a instância única do repositório. Se não ela não existir ainda, criar uma nova:
            get { return _movieRepository = _movieRepository ?? new MovieRepository(context); }
        }
        public IBookRepository BookRepository
        {
            // Usar a instância única do repositório. Se não ela não existir ainda, criar uma nova:
            get { return _bookRepository = _bookRepository ?? new BookRepository(context); }
        }

        public AppDbContext context;

        public UnitOfWOrk(AppDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        // Libera recursos associados ao contexto do banco de dados (context)
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
