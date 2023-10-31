namespace AutoMapperRegistrar
{
	public struct MappingType
	{
		public MappingType(Type source, Type destination)
		{
			this.Source = source;
			this.Destination = destination;
		}

		public Type Source { get; }

		public Type Destination { get; }
	}
}
