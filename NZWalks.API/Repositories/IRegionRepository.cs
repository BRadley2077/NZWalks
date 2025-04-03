using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(Guid id);
        Task CreateAsync(Region region);
        Task<Region> UpdateAsync(Guid id, UpdateRegionRequestDto updateRegionRequestDto);
        Task<Region> DeleteAsync(Guid id);
    }
}
