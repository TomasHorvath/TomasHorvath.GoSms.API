using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomasHorvath.GoSms.API.Model.Common;

namespace TomasHorvath.GoSms.API.Model
{
	public class SmsMessage
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("recipients")]
		public string[] Recipients { get; set; }

		[JsonProperty("channel")]
		public int Channel { get; set; }

		[JsonConverter(typeof(GSmsDateAdapter))]
		[JsonProperty("expectedSendStart")]
		public DateTime expectedSendStart { get; set; }
	}

}
