using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TomasHorvath.GoSms.API.Model;
using TomasHorvath.GoSms.API.Model.Organization;

namespace TomasHorvath.GoSms.API
{

	public class GoSmsConnector
	{
		private string APIUrl { get; set; }
		public AccessToken AccessToken { get; set; }
		internal static RestClient Client { get; private set; }
		private string ClientID;
		private string ClientSecret;

		static GoSmsConnector()
		{
			Client = new RestClient();
		}

		public GoSmsConnector(string APIUrl, string clientid, string clientsecret, bool setSecurityProtocol = true, WebProxy proxy = null)
		{
			if (setSecurityProtocol)
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			}
			Client.BaseUrl = new Uri(APIUrl);
			ClientID = clientid;
			ClientSecret = clientsecret;
			Client.UserAgent = "GoSms .NET Client ";

			if(proxy != null)
			{
				Client.Proxy = proxy;
			}

		}

		public GoSmsConnector GetAppToken()
		{
			return GetAppToken(OAuth.SCOPE_USER);
		}

		public GoSmsConnector GetAppToken(string scope)
		{
			var restRequest = new RestSharp.Newtonsoft.Json.RestRequest(@"/oauth/v2/token", Method.POST);
			restRequest.RequestFormat = DataFormat.Json;
			restRequest.AddHeader("Accept", "application/json");
			restRequest.JsonSerializer.ContentType = "application/x-www-form-urlencoded";
			restRequest.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
			var authenticator = new HttpBasicAuthenticator(ClientID, ClientSecret);
			authenticator.Authenticate(Client, restRequest);
			var response = Client.Execute(restRequest);
			AccessToken = Deserialize<AccessToken>(response.Content);
			return this;
		}

		#region detail organizace
		/// <summary>
		/// Detail organizace vrací aktuální stav kreditu a přehled nastavených komunikačních kanálů. Stav kreditu je vždy záporný, pokud Vaše organice má fakturaci přes tarif.
		/// https://doc.gosms.cz/#detail-organizace
		/// </summary>
		/// <returns></returns>
		public Organization GetOrganizationInfo()
		{
			var restRequest = CreateRestRequest(@"/api/v1/", "application/json", null, Method.GET);
			var response = Client.Execute(restRequest);
			return ProcessResponse<Organization>(response);
		}

		/// <summary>
		/// Detail organizace vrací aktuální stav kreditu a přehled nastavených komunikačních kanálů. Stav kreditu je vždy záporný, pokud Vaše organice má fakturaci přes tarif.
		/// https://doc.gosms.cz/#detail-organizace
		/// </summary>
		/// <returns></returns>
		public async Task<Organization> GetOrganizationInfoAsync()
		{
			var restRequest = CreateRestRequest(@"/api/v1/", "application/json", null, Method.GET);
			var response = await Client.ExecuteTaskAsync(restRequest);
			return await Task.Factory.StartNew(() => ProcessResponse<Organization>(response));
		}
		#endregion

		#region Test SMS

		/// <summary>
		/// Testovací metoda vrací vrací stejné chyby a při úspěchu detail zprávy jako při skutečném odesílání. 
		/// https://doc.gosms.cz/#testovaci-vytvoreni-zpravy-bez-odeslani
		/// </summary>
		/// <param name="message"></param>
		public DetailSmsResponse TestSMS(SmsMessage message)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/test", "application/json");
			restRequest.AddJsonBody(message);
			var response = Client.Execute(restRequest);
			return ProcessResponse<DetailSmsResponse>(response);
		}

		/// <summary>
		/// Testovací metoda vrací vrací stejné chyby a při úspěchu detail zprávy jako při skutečném odesílání. 
		/// https://doc.gosms.cz/#testovaci-vytvoreni-zpravy-bez-odeslani
		/// </summary>
		/// <param name="message"></param>
		public async Task<DetailSmsResponse> TestSMSAsync(SmsMessage message)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/test", "application/json");
			restRequest.AddJsonBody(message);

			var response = await Client.ExecuteTaskAsync(restRequest);
			return await Task.Factory.StartNew(() => ProcessResponse<DetailSmsResponse>(response));

		}

		#endregion

		#region send SMS 

		/// <summary>
		/// Testovací metoda vrací vrací stejné chyby a při úspěchu detail zprávy jako při skutečném odesílání. 
		/// https://doc.gosms.cz/#testovaci-vytvoreni-zpravy-bez-odeslani
		/// </summary>
		/// <param name="message"></param>
		public SmsResponse SendSMS(SmsMessage message)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/", "application/json");
			restRequest.AddJsonBody(message);
			var response = Client.Execute(restRequest);
			return ProcessResponse<SmsResponse>(response);
		}

		/// <summary>
		/// Testovací metoda vrací vrací stejné chyby a při úspěchu detail zprávy jako při skutečném odesílání. 
		/// https://doc.gosms.cz/#testovaci-vytvoreni-zpravy-bez-odeslani
		/// </summary>
		/// <param name="message"></param>
		public async Task<SmsResponse> SendSMSAsync(SmsMessage message)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/", "application/json");
			restRequest.AddJsonBody(message);

			var response = await Client.ExecuteTaskAsync(restRequest);
			return await Task.Factory.StartNew(() => ProcessResponse<SmsResponse>(response));
		}

		#endregion

		#region Delete message 

		public bool DeleteMessage(int id)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/{id}", "multipart/form-data");
			restRequest.AddParameter("id", id, ParameterType.UrlSegment);
			restRequest.Method = Method.DELETE;
			var response = Client.Execute(restRequest);

			if (response.StatusCode == HttpStatusCode.OK)
				return true;

			return false;
		}

		public async Task<bool> DeleteMessageAsync(int id)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/{id}", "multipart/form-data");
			restRequest.AddParameter("id", id, ParameterType.UrlSegment);
			restRequest.Method = Method.DELETE;

			var response = await Client.ExecuteTaskAsync(restRequest);
			return await Task.Factory.StartNew(() =>
			{
				if (response.StatusCode == HttpStatusCode.OK)
					return true;

				return false;
			});

		}

		#endregion

		#region Message detail 

		public DetailSmsResponse GetMessageById(int id)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/{id}", "multipart/form-data");
			restRequest.AddParameter("id", id, ParameterType.UrlSegment);
			restRequest.Method = Method.GET;
			var response = Client.Execute(restRequest);

			return ProcessResponse<DetailSmsResponse>(response);
		}

		public async Task<DetailSmsResponse> GetMessageByIdAsync(int id)
		{
			var restRequest = CreateRestRequest(@"/api/v1/messages/{id}", "multipart/form-data");
			restRequest.AddParameter("id", id, ParameterType.UrlSegment);
			restRequest.Method = Method.GET;

			var response = await Client.ExecuteTaskAsync(restRequest);
			return await Task.Factory.StartNew(() => ProcessResponse<DetailSmsResponse>(response));

		}

		#endregion

		#region Common 

		private T ProcessResponse<T>(IRestResponse response)
		{
			return Deserialize<T>(response.Content);
		}

		private T Deserialize<T>(string Content)
		{

			var err = JsonConvert.DeserializeObject<APIError>(Content);
			if (err.ErrorMessages != null)
			{
				throw new GoSmsClientException() { Error = err };
			}

			return JsonConvert.DeserializeObject<T>(Content);
		}

		private T DeserializeComplex<T>(string Content)
		{
			if (Content.Contains("error_code"))
			{
				Deserialize<APIError>(Content);
			}

			return JsonConvert.DeserializeObject<T>(Content);
		}


		private IRestRequest CreateRestRequest(string url, string contentType)
		{
			return CreateRestRequest(url, contentType, null);

		}

		private IRestRequest CreateRestRequest(string url, string contentType, Parameter parameter, Method method = Method.POST)
		{

			var restRequest = new RestSharp.Newtonsoft.Json.RestRequest(url, method);

			if (parameter != null)
			{
				restRequest.AddParameter(parameter);
			}
			restRequest.AddHeader("Accept", "application/json");

			if (contentType != null)
			{
				restRequest.AddHeader("Content-Type", contentType);
			}

			restRequest.AddHeader("Authorization", "Bearer " + AccessToken.Token);
			return restRequest;
		}

		#endregion
	}
}