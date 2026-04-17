using BranchERP.Application.Interfaces;
using BranchERP.Domain.Entities;

namespace BranchERP.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<string, object> _repositories = new();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var typeName = typeof(T).Name;

            if (_repositories.ContainsKey(typeName))
                return (IGenericRepository<T>)_repositories[typeName];

            var repo = new GenericRepository<T>(_context);
            _repositories.Add(typeName, repo);
            return repo;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
