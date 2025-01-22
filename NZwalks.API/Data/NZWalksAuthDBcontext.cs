using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalks.API.Data
{
	public class NZWalksAuthDBcontext : IdentityDbContext
	{
		public NZWalksAuthDBcontext(DbContextOptions<NZWalksAuthDBcontext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			var readerRoleId = "8c38ab1e-3c7a-42a2-b29e-6e0bc24ad31a";
			var writeRoleID = "fc4a69ac-525a-4b93-a436-1da775ea4d8d";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{ 
					Id=readerRoleId,   
					ConcurrencyStamp=readerRoleId,
					Name="Reader",
					NormalizedName="Reader".ToUpper()
				},
                new IdentityRole
				{
					Id=writeRoleID,
					ConcurrencyStamp=writeRoleID,
					Name="Writer",
					NormalizedName="writer".ToUpper()

				}

			};

			builder.Entity<IdentityRole>().HasData(roles);
		}
		
	}
}
