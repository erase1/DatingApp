
using API.Entities;

namespace API.Interfaces
{
    //this is the contract between the interface and implementation
    public interface ITokenService
    {
        string CreateToken(AppUser user);    
    }
}