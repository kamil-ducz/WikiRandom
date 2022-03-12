using System.Threading.Tasks;

namespace WebApplication4.Services
{
    /// <summary>
    /// Works on the Internet
    /// </summary>
    public interface IWikiService
    {
        Task<string> GetWikiRandom();

    }
}
