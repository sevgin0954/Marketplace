using AutoMapper;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.API
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			this.CreateMap<Id, string>()
				.ConvertUsing(i => i.Value);

			this.CreateMap<Price, decimal>()
				.ConvertUsing(p => p.Value);

			this.CreateMap<string, Email>()
				.ConvertUsing(str => new Email(str));
		}
	}
}
