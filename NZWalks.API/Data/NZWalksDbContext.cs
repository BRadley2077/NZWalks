using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>
            {
                new()
                {
                    Id = Guid.Parse("04f7b129-765e-4ae7-9436-6210e90172f8"),
                    Name = "Easy"
                },
                new()
                {
                    Id = Guid.Parse("240c310f-14bf-4c80-b43e-58e246fb5054"),
                    Name = "Medium"
                },
                new()
                {
                    Id = Guid.Parse("1ebec00c-25ed-430c-b731-f829a03daffc"),
                    Name = "Hard"
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
            {
                new()
                {
                    Id = Guid.Parse("0c70d4ea-ba81-4fd7-a930-b5b625acc932"),
                    Code = "KENT",
                    Name = "Kent Region",
                    RegionImageUrl = "https://imgur.com/a/bVZTnjj"
                },
                new()
                {
                    Id = Guid.Parse("682bb6ab-3da6-4224-bf30-1b54d34920cf"),
                    Code = "RAVN",
                    Name = "Ravenna Region",
                    RegionImageUrl = "https://imgur.com/a/gCRsu1E"
                },
                new()
                {
                    Id = Guid.Parse("d8cacfcd-59a7-48bd-9bfa-6cfb917718f5"),
                    Code = "AKRO",
                    Name = "Akron Region",
                    RegionImageUrl = "https://imgur.com/a/cMTL2Xo"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
