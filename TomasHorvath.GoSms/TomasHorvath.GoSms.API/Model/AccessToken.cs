using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasHorvath.GoSms.API.Model
{
	public class AccessToken
	{
		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("access_token")]
		public string Token { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonProperty("expires_in")]
		public long ExpiresIn { get; set; }
	}
}
