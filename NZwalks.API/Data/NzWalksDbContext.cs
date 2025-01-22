﻿using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using System.Security.Principal;

namespace NZwalks.API.Data
{
	public class NzWalksDbContext:DbContext
	{
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> dbContextOptions):base(dbContextOptions)
        {
                
        }
        public DbSet<Difficulty> Difficulties{ get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walks> Walks{ get; set; }
        public DbSet <Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//seed the data for difficulties
			//Easy,Medium,Hard
			var difficulties = new List<Difficulty>()
			{
				new Difficulty()
				{
					Id = Guid.Parse("99bffc36-9b49-4fa3-8697-a7e33b190619"),
					Name = "Easy"
				},
				new Difficulty()
				{
					Id = Guid.Parse("a1ee0a51-cc3a-4bc3-b1d7-9b1624a289ec"),
					Name = "Medium"
				},
				new Difficulty()
				{
					Id = Guid.Parse("2d0d623d-9c41-4f3f-929c-9aeb153bac1c"),
					Name = "Hard"
				}
			};

			//seed difficulty to the database
			modelBuilder.Entity<Difficulty>().HasData(difficulties);
			//seed data for region
			var regions = new List<Region>()
			{
				new Region()
				{
					Id=Guid.Parse("2c39c2b6-a5ff-42e6-85af-78ddbe71d073"),
					Name="Auckland",
					Code="AKL",
					RegionImageUrl="Auckland.jpg"
				},
					 new Region
				{
					Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
					Name = "Northland",
					Code = "NTL",
					RegionImageUrl = null
				},
				new Region
				{
					Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
					Name = "Bay Of Plenty",
					Code = "BOP",
					RegionImageUrl = null
				},
				new Region
				{
					Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
					Name = "Wellington",
					Code = "WGN",
					RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Region
				{
					Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
					Name = "Nelson",
					Code = "NSN",
					RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Region
				{
					Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
					Name = "Southland",
					Code = "STL",
					RegionImageUrl = null
				}
			};
		   modelBuilder.Entity<Region>().HasData(regions);
		}

	}
}
