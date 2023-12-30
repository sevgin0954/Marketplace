using AutoMapper;
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
		}
	}
}
