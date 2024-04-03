using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoMapperRegistrar.Tests")]

namespace AutoMapperRegistrar
{
	public struct MappingType
	{
		internal MappingType(Type source, Type destination, bool isReversible = false)
		{
			this.Source = source;
			this.Destination = destination;
			this.IsReversible = isReversible;
		}

		public Type Source { get; }

		public Type Destination { get; }

		public bool IsReversible { get; internal set; } = false;
	}
}
