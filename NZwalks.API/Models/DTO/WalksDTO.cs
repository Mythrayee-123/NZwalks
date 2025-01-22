namespace NZwalks.API.Models.DTO
{
	public class WalksDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }	
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }
		//public Guid DifficultyID { get; set; }
		//public Guid RegionId { get; set; }
		//Now that region and difficulty has all the info related to the class  
        public RegionDTO Region { get; set; }
	
        public DifficultyDTO Difficulty { get; set; }
    }
}
