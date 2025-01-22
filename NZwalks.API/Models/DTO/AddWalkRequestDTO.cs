using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
	public class AddWalkRequestDTO
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[Range(0,50)]
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }
		[Required]
		public Guid DifficultyID { get; set; }
		[Required]
		public Guid RegionId { get; set; }
	}
}
