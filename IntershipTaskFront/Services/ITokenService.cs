using System.Threading.Tasks;
using IntershipTaskFront.Models;

namespace IntershipTaskFront.Services
{
    public interface ITokenService
    {
        public UserData GetCredential();

        public Task<string> GetToken(UserData userData);
    }
}