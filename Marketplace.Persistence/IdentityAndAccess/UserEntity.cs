using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;

namespace Marketplace.Persistence.IdentityAndAccess
{
	public class UserEntity : IMappableBothDirections<User>, ICustomMappings
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public bool IsAdmin { get; set; }

		public string PasswordHash { get; set; }

		public string PasswordSalt { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<User, UserEntity>()
				.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password.Hash))
				.ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => src.Password.Salt))
				.ReverseMap();
		}
	}
}
