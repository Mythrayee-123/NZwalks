using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
	public class RegionsController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;

		public RegionsController(IHttpClientFactory httpClientFactory)
        {
			this.httpClientFactory = httpClientFactory;
		}
		[HttpGet]
        public async Task<IActionResult> Index()
		{
			List<RegionsDto> response = new List<RegionsDto>();
			try
			{
				//Get all regions from web Api
				var client = httpClientFactory.CreateClient();
				var httpResponseMessage = await client.GetAsync("https://localhost:7176/api/Regions");
				httpResponseMessage.EnsureSuccessStatusCode();
			response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionsDto>>());
				
			}
			catch (Exception ex)
			{
				//Log the exception

				throw;
			}
			return View(response);
		}
		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		//post the added data
		public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
		{
			var client = httpClientFactory.CreateClient();
			var httpRequestMessage=new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri=new Uri("https://localhost:7176/api/Regions"),
				Content=new StringContent(JsonSerializer.Serialize(addRegionViewModel),Encoding.UTF8,"application/json")	
			};	
			var Response=await client.SendAsync(httpRequestMessage);
			Response.EnsureSuccessStatusCode();
		    var result=await Response.Content.ReadFromJsonAsync<RegionsDto>();
			if(result is not null)
			{
				return RedirectToAction("Index", "Regions");
			}
			return View();
		}
		[HttpGet]
		//get the regions by id and take it to the edit screen
		public async Task<IActionResult> Edit(Guid id)
		{
			var client = httpClientFactory.CreateClient();
		var response=	await client.GetFromJsonAsync<RegionsDto>($"https://localhost:7176/api/Regions/{id}");
			if (response is not null)
			{
				return View(response);
			}
			return View(null);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(RegionsDto request)
		{
			var client = httpClientFactory.CreateClient();
			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"https://localhost:7176/api/Regions/{request.Id}"),
				Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
			};
		var httpResponseMessage=await client.SendAsync(httpRequestMessage);
			httpResponseMessage.EnsureSuccessStatusCode();
			var result = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDto>();
			if(result is not null)
			{
				return RedirectToAction("Edit", "Regions");
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Delete(RegionsDto request)
		{
			try
			{
				var client = httpClientFactory.CreateClient();
				var httpResponseMessage = await client.DeleteAsync($"https://localhost:7176/api/Regions/{request.Id}");
				httpResponseMessage.EnsureSuccessStatusCode();
				return RedirectToAction("Index", "Regions");
			}
			catch (Exception ex)
			{

				throw;
			}
			return View("Edit");
		}
	}
}
