using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZwalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase

	{
		private readonly IMapper mapper;
		private readonly IWalkRepository walkRepository;

		public WalksController(IMapper mapper, IWalkRepository walkRepository)
		{
			this.mapper = mapper;
			this.walkRepository = walkRepository;
		}
		//create walk
		[HttpPost]
		[ValidateModel]//use validate model before the data enters into the API
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
		{
			
				//Map the DTO to the domain
				var walkDomain = mapper.Map<Walks>(addWalkRequestDTO);
				//add the creted field to the domain
				await walkRepository.CreateAsync(walkDomain);
				//return the ok result DTO
				//map domain to DTO

				return Ok(mapper.Map<WalksDTO>(walkDomain));
			
		}
		//Get all the walks
		//GET:/api/Walks?filteron=Name&filterQuery=Track&&sortBy=Name && isAscending=true&&pagenumber=1&&pagesize=1000
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery] string? filterquery,
							 [FromQuery]string? sortBy, [FromQuery]bool? isAscending ,
							 [FromQuery]int pageNumber = 1,[FromQuery] int pageSize=1000)
		{
			var walksDomain = await walkRepository.GetAllAsync(filterOn, filterquery,sortBy, isAscending?? true,
				pageNumber,pageSize);

			//create an exception using globalexception middleware
			//throw new Exception("Something went wrong");
			//If any thing bad happens our global exception handler middle ware 
			//take care of the exception
				//map the domain to DTO
			return Ok(mapper.Map<List<WalksDTO>>(walksDomain));


		}
		//Get walk bt ID
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetWalksbyId([FromRoute]Guid id)
		{
			var walkDomain= await walkRepository.GetByIdAsync(id);
			if (walkDomain == null)
			{
				return NotFound();
			}
			//map the domain to DTO
			return Ok(mapper.Map<WalksDTO>(walkDomain));
		}
		//Update by id
		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]//use validate model before the data enters into the API
		public async Task<IActionResult>Update([FromRoute]Guid id, [FromBody] UpdateWalksDTO updateWalksDTO)
		{
			
				//map the dto to the domain
				var walkDomain = mapper.Map<Walks>(updateWalksDTO);

				walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
				if (walkDomain == null)
				{ return NotFound(); }

				//map the updated domain to DTO
				return Ok(mapper.Map<WalksDTO>(walkDomain));
			
						
		}
		//Delete
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult>Delete([FromRoute]Guid id)
		{
			var deletedWalkDomain=await walkRepository.DeleteAsync(id);
			if (deletedWalkDomain  == null)	
				{ return NotFound(); }
			//map the domain to DTO
			return Ok(mapper.Map<WalksDTO>(deletedWalkDomain));
		}
	}
}
