using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasHorvath.GoSms.API.Model
{

	public class DetailSmsResponse
	{
		public string messageType { get; set; }
		public Message message { get; set; }
		public int channel { get; set; }
		public Stats stats { get; set; }
		public Sendinginfo sendingInfo { get; set; }
		public Reply reply { get; set; }
	}

	public class Message
	{
		public string fulltext { get; set; }
		public string[] parts { get; set; }
	}

	public class Stats
	{
		public float price { get; set; }
		public bool hasDiacritics { get; set; }
		public int smsCount { get; set; }
		public int messagePartsCount { get; set; }
		public int recipientsCount { get; set; }
		public Numbertypes numberTypes { get; set; }
	}

	public class Numbertypes
	{
		public int czMobile { get; set; }
		public int czOther { get; set; }
		public int sk { get; set; }
		public int other { get; set; }
	}

	public class Sendinginfo
	{
		public string status { get; set; }
		public DateTime expectedSendStart { get; set; }
		public string sentStart { get; set; }
		public string sentFinish { get; set; }
	}

	public class Reply
	{
		public bool hasReplies { get; set; }
		public int repliesCount { get; set; }
	}

}
