using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
	public class SqlWalksRepository : IWalkRepository
	{
		private readonly NzWalksDbContext dbContext;

		public SqlWalksRepository(NzWalksDbContext dbContext)
        {
			this.dbContext = dbContext;
		}
        public async Task<Walks> CreateAsync(Walks Walks)
		{
			await dbContext.Walks.AddAsync(Walks);
			await dbContext.SaveChangesAsync();
			return Walks;
		}

		public async Task<Walks?> DeleteAsync(Guid id)
		{
			var existingwalks = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
			if (existingwalks == null)
			{
				return null;
			}
			dbContext.Walks.Remove(existingwalks);
			await dbContext.SaveChangesAsync();
			return existingwalks;
		}

		public async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
			    string? sortBy=null,bool isAscending=true,
				int pageNumber=1,int pageSize=1000)
			
		{
			//Changing this as queriable instead of list
			var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
			//Filtering
			if(string.IsNullOrWhiteSpace(filterOn)==false&& (string.IsNullOrWhiteSpace(filterQuery) == false))
			{
				if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks=walks.Where(x=>x.Name.Contains(filterQuery));
				}
			}
			//sorting
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
				}
				else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
					walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
				}
			}
			//pagenation
			//If the page number is 1,and page sixe =10;then the skip result willbe Zero
			//it means first 10 results
			//If the page number is 2,and page sixe =10;then the skip result willbe 10
			//it means skip 10 results and give second batch of 10
			var skipResult = (pageNumber - 1) * pageSize;
			return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
			//want to get related info of difficulty & region;populate the fields from
			// the navigation propert."include" syntax to add the navigation property

			//return await dbContext.Walks
			//	.Include("Difficulty")
			//	.Include("Region")
			//	.ToListAsync();	
		}
			

		public async Task<Walks?> GetByIdAsync(Guid id)
		{
			return await dbContext.Walks
				.Include("Difficulty")
				.Include("Region")
				.FirstOrDefaultAsync(x => x.Id == id);
			

		}

		public async Task<Walks?> UpdateAsync(Guid id, Walks Walks)
		{
			var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
			if (existingWalk == null)
			{
				return null;

			}
			existingWalk.Name= Walks.Name;	
			existingWalk.LengthInKm = Walks.LengthInKm;	
			existingWalk.WalkImageUrl = Walks.WalkImageUrl;
			existingWalk.DifficultyID = Walks.DifficultyID;
			existingWalk.RegionId = Walks.RegionId;

			await dbContext.SaveChangesAsync();
			return existingWalk;	

		}
	}
}
