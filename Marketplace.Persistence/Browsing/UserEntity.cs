using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.Browsing;
using Marketplace.Domain.Browsing.UserAggregate;

namespace Marketplace.Persistence.Browsing
{
	public class UserEntity : IMappableBothDirections<User>, ICustomMappings
	{
		public string Id { get; set; }

		public ICollection<ViewEntity> Views { get; set; }

		public ICollection<SearchEntity> Searches { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.ShouldMapField = p => p.IsPublic || p.IsPrivate;

			configuration.CreateMap<User, UserEntity>()
				.ForMember(dest => dest.Searches, opt => opt.MapFrom("recommendedSearchesAndSearchCount"));

			configuration.CreateMap<KeyValuePair<Search, int>, SearchEntity>()
				.ConvertUsing((src, dest, ctx) =>
				{
					var result = ctx.Mapper.Map<SearchEntity>(src.Key);
					result.SearchCount = src.Value;
					return result;
				});
		}
	}
}