using NZwalks.API.Models.Domain;
using System.Net;

namespace NZwalks.API.Repositories
{
	public interface IImageRepository
	{
		Task<Image>Upload(Image image);
	}
}
