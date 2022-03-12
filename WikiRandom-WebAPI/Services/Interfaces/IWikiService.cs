using System.Threading.Tasks;

namespace WebApplication4.Services.Interfaces
{
    /// <summary>
    /// Works on the Internet
    /// </summary>
    public interface IWikiService
    {
        Task<string> GetWikiRandom();

    }
}
