using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomasHorvath.GoSms.API.Model;

namespace TomasHorvath.GoSms.API
{
	public class GoSmsClientException : Exception
	{
		public APIError Error { get; set; }
	}
}
