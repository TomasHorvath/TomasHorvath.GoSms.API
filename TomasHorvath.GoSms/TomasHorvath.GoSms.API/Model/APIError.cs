using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomasHorvath.GoSms.API.Model.Common;

namespace TomasHorvath.GoSms.API.Model
{
	public class APIError
	{
		[JsonProperty("date_issued")]
		[JsonConverter(typeof(GSmsDateAdapter))]
		public DateTime DateIssued { get; set; }

		[JsonProperty("errors")]
		public IList<ErrorElement> ErrorMessages { get; set; }


	}
}
