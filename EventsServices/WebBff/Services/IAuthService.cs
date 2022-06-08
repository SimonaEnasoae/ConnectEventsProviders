using GrpcAuth;
using System.Threading.Tasks;
using WebBff.Controllers.Responses;
using LoginResponse = WebBff.Controllers.Responses.LoginResponse;

namespace WebBff.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(UserAuth user);
        Task<LogoutResponse> Logout(UserAuth user);

    }
}
