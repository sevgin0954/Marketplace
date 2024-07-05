using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
    public interface IRepository<T, TId>
        where T : AggregateRoot
        where TId : Id
    {
        void Add(T aggregate);

        void Remove(T aggregate);

        Task<T> GetByIdAsync(TId id);

        ICollection<T> GetAll();

        Task<bool> CheckIfExistAsync(TId id);

        Task<ICollection<T>> WhereAsync(Expression<Func<T, bool>> predicate);

        void Update(T aggregate);

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}