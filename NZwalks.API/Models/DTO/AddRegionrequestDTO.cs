using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
	public class AddRegionrequestDTO
	{
		[Required]
		[MinLength(3,ErrorMessage ="code has to be a minimum of 3 charectors")]
		[MaxLength(3, ErrorMessage = "code has to be a max of 3 charectors")]
		public string Code { get; set; }
		[Required]
		[MaxLength(100, ErrorMessage = "Name has to be a max of 100 charectors")]
		public string Name { get; set; }
		public string? RegionImageUrl { get; set; }
	}
}
