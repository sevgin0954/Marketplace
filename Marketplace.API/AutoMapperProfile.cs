using AutoMapper;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.API
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			this.CreateMap<Id, string>()
				.ForMember(dest => dest, config => config.MapFrom(src => src.Value));

			this.CreateMap<Price, decimal>()
				.ForMember(dest => dest, config => config.MapFrom(src => src.Value));
		}
	}
}
