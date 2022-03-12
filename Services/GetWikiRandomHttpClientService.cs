using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class GetWikiRandomHttpClientService : IGetWikiRandom
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        readonly HttpClient client = new HttpClient();

        public async Task<string> GetWikiRandom()
        {
            HttpResponseMessage response = await client.GetAsync(Helpers.URL); //UrL should be in different place in project-solution
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            string title = WebServices_methods.GetBetween(responseBody, "<title>", " - Wikipedia");
            return title;
        }
    }
}
