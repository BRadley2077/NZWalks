using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id) ?? new Region();
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return (await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id)) ?? new Region();
        }

        public async Task<Region> UpdateAsync(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            if (existingRegion != null) 
            {
                existingRegion.Code = updateRegionRequestDto.Code;
                existingRegion.Name = updateRegionRequestDto.Name;
                existingRegion.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

                await dbContext.SaveChangesAsync();

                return existingRegion;
            }

            return new Region();
        }
    }
}
