using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TomasHorvath.GoSms.API;

namespace TomasHorvath.GoSms.Console
{
	class Program
	{
		static void Main(string[] args)
		{

			string clientId = "paste your clientId";
			string clientSecret = "paste your clientSecret";
			string phoneNumber = "+420111222333";

			// you can use fiddler proxy during implementation
			WebProxy fiddlerProxy = new WebProxy("127.0.0.1", 8888); 
			// with proxy
			GoSmsConnector connector = new GoSmsConnector("https://app.gosms.cz", clientId, clientSecret,proxy:fiddlerProxy);

			//without
			//GoSmsConnector connector = new GoSmsConnector("https://app.gosms.cz", clientId, clientSecret);

			connector.GetAppToken();

			var info = connector.GetOrganizationInfo();

			connector.TestSMS(new API.Model.SmsMessage() { Channel = info.channels.FirstOrDefault().id, expectedSendStart = DateTime.Now.AddMinutes(10), Message = "hello world", Recipients = new string[1] { phoneNumber } });

			connector.SendSMS(new API.Model.SmsMessage() { Channel = info.channels.FirstOrDefault().id, expectedSendStart = DateTime.Now.AddMinutes(1), Message = "hello world", Recipients = new string[1] { phoneNumber } });

			int messageId = 0;

			connector.DeleteMessage(messageId);

			var detail = connector.GetMessageById(messageId);
		}
	}
}
