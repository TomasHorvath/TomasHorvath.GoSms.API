using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasHorvath.GoSms.API.Model.Common
{
	public class GSmsDateAdapter : IsoDateTimeConverter
	{
		public GSmsDateAdapter()
		{
			DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
		}
	}
}
