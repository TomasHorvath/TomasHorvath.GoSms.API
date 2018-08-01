using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasHorvath.GoSms.API.Model.Organization
{

	public class Organization
	{
		public float currentCredit { get; set; }
		public Channel[] channels { get; set; }
	}

	public class Channel
	{
		public int id { get; set; }
		public string name { get; set; }
		public string sourceNumber { get; set; }
	}

}
