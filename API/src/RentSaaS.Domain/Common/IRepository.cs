//using System.Linq.Expressions;

//namespace RentSaaS.Domain.Base;

//public interface IRepository<T> where T : IEntity
//{
//    Task<IEnumerable<T>> GetAll();
//    Task<T> GetById(Guid id);
//    Task<bool> Add(T entity);
//    Task<bool> AddRangeAsync(T[] entities);
//    Task<bool> Delete(Guid id);
//    //Task<bool> Update(T entity);
//    Task<T> Update(T entity);
//    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
//    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
//    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
//    Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);

//    IQueryable<T> AsQueryable();
//    Task<IEnumerable<T>> GetAllPaginated(int page = 1, int pageSize = 10);
//    Task<int> Count(Expression<Func<T, bool>>? predicate = null);
//    Task<IEnumerable<T>> GetAllPaginatedFiltered(Expression<Func<T, bool>> filter,int page = 1,int pageSize = 10,string? sortBy = null,bool isAscending = true);
//    Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedResultWithCount(Expression<Func<T, bool>>? filter = null,int page = 1,int pageSize = 10);

//}
using RentSaaS.Domain.Base;
using System.Linq.Expressions;

namespace RentSaaS.Infrastructure.Data.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    IQueryable<T> AsQueryable();

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    // An alias for FindAsync
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllPaginatedAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllPaginatedFilteredAsync(
        Expression<Func<T, bool>> filter,
        int page = 1,
        int pageSize = 10,
        string? sortBy = null,
        bool isAscending = true,
        CancellationToken cancellationToken = default);

    Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedResultWithCountAsync(
        Expression<Func<T, bool>>? filter = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}