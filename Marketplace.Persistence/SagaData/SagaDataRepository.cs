using AutoMapper;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.SagaData
{
    public class SagaDataRepository : Repository<Domain.Common.SagaData, Id, SagaDataEntity>
	{
        public SagaDataRepository(SagaDataDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }
    }
}