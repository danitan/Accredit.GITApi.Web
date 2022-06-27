using System.Threading.Tasks;

namespace Accredit.GITAPI.Service
{
    public interface IGITAPIService
    {
        Task<string> GetUser(string username);
    }
}
