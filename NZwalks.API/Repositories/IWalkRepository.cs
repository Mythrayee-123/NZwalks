using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZwalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walks> CreateAsync(Walks Walks);
		Task<List<Walks>> GetAllAsync(string? filterOn=null,string? filterQuery=null
			,string? sortBy=null,bool isAscending=true,
			int pageNumber = 1,int pageSize = 1000);
		Task<Walks?> GetByIdAsync(Guid id);
		Task<Walks?> UpdateAsync(Guid id,Walks Walks);
		Task<Walks?>DeleteAsync(Guid id);
	}

}