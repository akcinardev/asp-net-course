using StocksAppExample.ServiceContracts;
using System.Text.Json;

namespace StocksAppExample.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using(HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader reader = new StreamReader(stream);

                string response = reader.ReadToEnd();
                Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDict == null)
                {
                    throw new InvalidOperationException("No response from Finnhub server");
                }

                if(responseDict.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDict["error"]));
                }

                return responseDict;
            }
        }
    }
}
