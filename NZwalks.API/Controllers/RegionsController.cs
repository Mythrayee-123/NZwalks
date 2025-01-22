using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Text.Json;

namespace NZwalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class RegionsController : ControllerBase
	{
		private readonly NzWalksDbContext dbContext;
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;
		private readonly ILogger<RegionsController> logger;

		public RegionsController(NzWalksDbContext dbContext
			,IRegionRepository regionRepository
			,IMapper mapper
			,ILogger<RegionsController> logger)
		{
			this.dbContext = dbContext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
			this.logger = logger;
		}
		[HttpGet]
		//Get all region
		//api/Regions
		//[Authorize(Roles ="Reader")]
		public async Task<IActionResult> GetAll()
		{
			logger.LogInformation("GetAll Action method was invoked");
			//Get the data from the database
			var regionDomain = await regionRepository.GetAllAsync();

			//Using Automapper
			//map the domain model to DTO using automapper
			//destination to source
			//return the DTO
			logger.LogInformation($"finished all region request data:{JsonSerializer.Serialize(regionDomain)}");
			return Ok(mapper.Map<List<RegionDTO>>(regionDomain));
		}
		[HttpGet]
		[Route("{id:Guid}")]
		//Get single region by id
		//[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{//get region from the database
			var region = await regionRepository.GetByIdAsync(id);
			//var region = dbContext.Regions.Find(id);
			if (region == null)
			{
				return NotFound();

			}
			
			//using auto mapper
			//destination from source
			//mapp from domain to dto
			return Ok(mapper.Map<RegionDTO>(region));
			
		}
		//Post
		[HttpPost]
		[ValidateModel]//use validate model before the data enters into the API
		//[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Create([FromBody] AddRegionrequestDTO addRegionrequestDTO)
		{		
			
				//use mapping to map dto to domain
				//destination from source
				var regionDomainModel = mapper.Map<Region>(addRegionrequestDTO);
				//use domain model to create region
				regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

				//map the domain model to DTO to display the result
				var regiondto = mapper.Map<RegionDTO>(regionDomainModel);
				
				return CreatedAtAction(nameof(GetById), new { id = regiondto.Id }, regiondto);

		
		}
		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]//use validate model before the data enters into the API
		//[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
		{
							//check whether the region exist
				//Map the Dto to Domain model
				//map to domain from DTO
				var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
				
				regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
				if (regionDomainModel == null)
				{
					return NotFound();
				}

				//Convert the Domain to DTO
				var regiondto = mapper.Map<RegionDTO>(regionDomainModel);
				
				return Ok(regiondto);
						
		}
		//Delete a region
		[HttpDelete]
		[Route("{id:Guid}")]
		//[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
		var region=await regionRepository.DeleteAsync(id);	
			if(region == null)
			{
				return BadRequest();	
			}
						
			//map the domain to dto
			
			
			return Ok(mapper.Map<RegionDTO>(region));
		}	
	}
}