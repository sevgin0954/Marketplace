using AutoMapper;
using Marketplace.Domain.Sales.MakeOfferSagaNS;

namespace Marketplace.Persistence.Sales
{
	public class MakeOfferSagaDataRepository : Repository<MakeOfferSagaData, MakeOfferSagaId, MakeOfferSagaDataEntity>
	{
		public MakeOfferSagaDataRepository(SalesDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}
