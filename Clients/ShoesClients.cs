using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace shoesAPI.Clients
{
    public class ShoesClient
    {
        private HttpClient _client;

        public ShoesClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetShoesbyBrands()
        {
            string shoesmodel = "nike dunk";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://sneaker-database-stockx.p.rapidapi.com/getproducts?keywords={shoesmodel}&limit=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "fb155d1efdmsh7908af97c0d69f7p1c0407jsnba69417537da" },
                    { "X-RapidAPI-Host", "sneaker-database-stockx.p.rapidapi.com" },
                },
            };

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}
