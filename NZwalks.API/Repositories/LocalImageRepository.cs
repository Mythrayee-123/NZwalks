using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using System.Linq.Expressions;

namespace NZwalks.API.Repositories
{
	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly NzWalksDbContext dbContext;

		public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
			NzWalksDbContext dbContext )
        {
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.dbContext = dbContext;
		}
        public async Task<Image> Upload(Image image)
		{
			var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
				$"{image.FileName}{image.FileExtension}");
			//Upload image to the local path
			using var stream = new FileStream(localFilePath, FileMode.Create);
			await image.File.CopyToAsync(stream);
			//formatt the path
			//https://localhost:1234/images/image.jpg
			var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}"; 
				
			image.FilePath = urlFilePath;	
			//Add the image to the images table
			await dbContext.Images.AddAsync(image);
			await dbContext.SaveChangesAsync();	
			return image;	

				
		}
	}
}
