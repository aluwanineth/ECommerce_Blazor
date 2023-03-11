using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Account;
using ECommerce.Application.Wrappers;

namespace ECommerce.App.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task<string> RefreshToken();
        Task Logout();
    }
}
