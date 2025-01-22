using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace NZwalks.API.Repositories
{
	public interface ITokenRepository
	{
		string CreateJWTToken(IdentityUser user, List<string> roles);
	}
}
