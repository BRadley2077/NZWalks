using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id) {
            var selectedRegion = await regionRepository.GetByIdAsync(id);            

            if (selectedRegion.Id == Guid.Empty)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(selectedRegion));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]AddRegionRequestDto regionRequestDto)
        {
            var newRegion = mapper.Map<Region>(regionRequestDto);

            await regionRepository.CreateAsync(newRegion);

            var newRegionDto = mapper.Map<RegionDto>(newRegion);

            return CreatedAtAction(nameof(GetById), new {id = newRegionDto.Id}, newRegionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegion = await regionRepository.UpdateAsync(id, updateRegionRequestDto);

            return Ok(mapper.Map<RegionDto>(existingRegion));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region.Id == Guid.Empty)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
