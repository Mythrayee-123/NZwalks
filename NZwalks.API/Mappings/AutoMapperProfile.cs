using AutoMapper;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Mappings
{
	public class AutoMapperProfile:Profile
	{
        public AutoMapperProfile()
        {
            //source,destination
               CreateMap<Region,RegionDTO> ().ReverseMap();
               CreateMap<AddRegionrequestDTO,Region> ().ReverseMap();
               CreateMap<UpdateRegionRequestDTO,Region> ().ReverseMap();
               CreateMap<AddWalkRequestDTO,Walks> ().ReverseMap();
               CreateMap<Walks, WalksDTO>().ReverseMap();
               CreateMap <Difficulty,DifficultyDTO> ().ReverseMap();
			   CreateMap<UpdateWalksDTO,Walks>().ReverseMap();
		}
    }
}
