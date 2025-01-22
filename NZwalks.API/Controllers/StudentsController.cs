using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZwalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		[HttpGet]	
		public IActionResult GetAllStudents()
		{
			string[] studentName=new string[] {"john","Jane","Mark","Emily","David"};
			
			return Ok(studentName);

		}
	}
}
