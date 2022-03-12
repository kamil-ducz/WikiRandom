using System.Threading.Tasks;

namespace ServicesOld
{
    public interface IGetWikiRandom //interface supports retreving random article's header from Wikipedia
    {
        Task<string> GetWikiRandom();   //one async method to handle the request
    }
}
