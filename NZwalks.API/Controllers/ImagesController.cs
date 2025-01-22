using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
        {
			this.imageRepository = imageRepository;
		}
		//post
		//api/Image/Upload
		[HttpPost]
        [Route("Upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto RequestDto)
		{
			
			ValidateFileUpload(RequestDto);
			if(ModelState.IsValid) 
				{
				//convert DTO to domain model
				var imageDomainModel = new Image
				{
					File = RequestDto.File,
					FileExtension = Path.GetExtension(RequestDto.File.FileName),
					FileSizeInBytes = RequestDto.File.Length,
					FileName = RequestDto.FileName,	
					FileDescription = RequestDto.FileDescription,
				};


				//use Repository to upload
				await imageRepository.Upload(imageDomainModel);
				return Ok(imageDomainModel);
			}
			return BadRequest(ModelState);
		}
		private void ValidateFileUpload(ImageUploadRequestDto request)
		{
			var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
			if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
			{
				ModelState.AddModelError("file", "UnSupported File Extension");
			}
			if (request.File.Length > 10485760)
			{
				ModelState.AddModelError("file", " File Size is more");
			}
			}

	}
}
