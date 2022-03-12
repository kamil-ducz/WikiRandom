using Services;
using System.Threading.Tasks;

namespace WebApplication4.Services.Classes
{
    public class WikiServiceHTTPClient : IWikiService
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        readonly HttpClient client = new HttpClient();

        public async Task<string> GetWikiRandom()
        {
            HttpResponseMessage response = await client.GetAsync(ConstDefinitions.URL); //UrL should be in different place in project-solution
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            string title = HelperMethods.GetBetween(responseBody, "<title>", " - Wikipedia</title>");
            return title;
        }
    }
}
