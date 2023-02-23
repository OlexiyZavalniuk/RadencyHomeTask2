using AutoMapper;

using Models;


namespace Core
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Book0, Book>();
		}
	}
}
