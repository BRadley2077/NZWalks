using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets All Regions
        /// </summary>
        /// <returns>All Regions</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await _dbContext.Regions.ToListAsync();

            var regionsDto = new List<RegionDto>();

            foreach (var region in regions) 
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        /// <summary>
        /// Gets a single region by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single request region</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await _dbContext.Regions.FindAsync(id);

            if (region == null) 
            { 
                return NotFound(); 
            }

            var regionDto = new RegionDto
            {
                Id= region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl= region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDto requestDto)
        {
            var regionDomainModel = new Region
            {
                Code = requestDto.Code,
                Name = requestDto.Name,
                RegionImageUrl= requestDto.RegionImageUrl
            };

            await _dbContext.Regions.AddAsync(regionDomainModel);
            await _dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var regionsDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionsDomainModel == null)
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(regionsDomainModel);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
