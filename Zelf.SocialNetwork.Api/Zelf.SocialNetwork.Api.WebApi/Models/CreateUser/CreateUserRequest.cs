using System.ComponentModel.DataAnnotations;

namespace Zelf.SocialNetwork.Api.WebApi.Models.CreateUser
{
	public class CreateUserRequest
	{
		[Required]
		[RegularExpression("[a-zA-Zа-яА-Я0-9 ]{1,64}")]
		public string UserName { get; set; }
	}
}
