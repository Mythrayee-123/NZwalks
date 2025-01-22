using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZwalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZwalks.API.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>>GetAllAsync();
		Task<Region?> GetByIdAsync(Guid id);
		Task<Region> CreateAsync(Region region);
		Task<Region?>UpdateAsync(Guid id,Region region);
		Task <Region?>DeleteAsync(Guid id);
	}
}
