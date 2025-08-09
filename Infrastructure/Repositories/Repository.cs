using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            // AsNoTracking desabilita o tracking do EF Core, o que melhora o desempenho.
            // Útil em métodos de consulta apenas, onde não há a manipulação de dados.
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            //_context.SaveChanges(); // Save foi para o controller, atrávés do padrã Unit of Work
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            //_context.SaveChanges(); // Save foi para o controller, atrávés do padrã Unit of Work
            return entity;
        }

        public T? SoftDelete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null) return null;

            var prop = entity.GetType().GetProperty("IsDeleted");
            if (prop != null)
            {
                prop.SetValue(entity, true);
                //_context.SaveChanges(); // Save foi para o controller, atrávés do padrã Unit of Work
            }

            return entity;
        }
    }
}
