using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            var regionsDto = new List<RegionDto>();

            foreach (var region in regions) {

                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id) {
            var selectedRegion = dbContext.Regions.FirstOrDefault(region => region.Id == id);            

            if (selectedRegion == null)
            {
                return NotFound();
            }

            var selectionRegionDto = new RegionDto
            {
                Id = selectedRegion.Id,
                Code = selectedRegion.Code,
                Name = selectedRegion.Name,
                RegionImageUrl = selectedRegion.RegionImageUrl
            };

            return Ok(selectionRegionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]AddRegionRequestDto regionRequestDto)
        {
            var newRegion = new Region
            {
                Code = regionRequestDto.Code,
                Name = regionRequestDto.Name,
                RegionImageUrl = regionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(newRegion);
            dbContext.SaveChanges();

            var newRegionDto = new RegionDto
            {
                Id = newRegion.Id,
                Code = newRegion.Code,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new {id = newRegionDto.Id}, newRegionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(region => region.Id == id);

            if (existingRegion == null)
            {
                return NotFound();
            }

            existingRegion.Code = updateRegionRequestDto.Code;
            existingRegion.Name = updateRegionRequestDto.Name;
            existingRegion.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = existingRegion.Id,
                Code = existingRegion.Code,
                Name = existingRegion.Name,
                RegionImageUrl = existingRegion.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
