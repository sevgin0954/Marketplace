using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System.Linq.Expressions;

namespace Marketplace.Persistence.SagaData
{
    public class SagaDataRepository<TSagaData, TSagaId> : 
        Repository<TSagaData, TSagaId, SagaDataEntity>, 
        ISagaDataRepository<TSagaData, TSagaId>
            where TSagaData : Domain.Common.SagaData
		    where TSagaId : Id

	{
        public SagaDataRepository(SagaDataDbContext dbContext)
            : base(dbContext) { }

        public Task<ICollection<TSagaData>> FindAsync(Expression<Func<TSagaData, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}