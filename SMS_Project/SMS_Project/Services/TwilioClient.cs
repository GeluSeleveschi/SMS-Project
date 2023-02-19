using Twilio.Clients;
using Twilio.Http;
using SystemHttpClient = System.Net.Http.HttpClient;

namespace SMS_Project
{
    public class TwilioClient : ITwilioRestClient
    {
        private readonly ITwilioRestClient _client;

        public TwilioClient(ITwilioRestClient client)
        {
            _client = client;
        }

        public TwilioClient(IConfiguration config, SystemHttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "SMS-Project");

            _client = new TwilioRestClient(
                config["Twilio:AccountSid"],
                config["Twilio:AuthToken"],
                httpClient: new SystemNetHttpClient(httpClient));
        }

        public Response Request(Request request) => _client.Request(request);
        public Task<Response> RequestAsync(Request request) => _client.RequestAsync(request);
        public string AccountSid => _client.AccountSid;
        public string Region => _client.Region;
        public Twilio.Http.HttpClient HttpClient => _client.HttpClient;
    }
}
