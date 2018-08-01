using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasHorvath.GoSms.API
{

	public class SmsResponse
	{
		public Recipients recipients { get; set; }
		public string link { get; set; }
	}

	public class Recipients
	{
		public string[] invalid { get; set; }
	}

}
