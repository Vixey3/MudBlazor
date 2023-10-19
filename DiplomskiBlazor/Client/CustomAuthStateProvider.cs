using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DiplomskiBlazor.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());

            var korIme = await _localStorage.GetItemAsStringAsync("korisnik");
            //var korId = await _localStorage.GetItemAsStringAsync("korId");

            if (!string.IsNullOrEmpty(korIme))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, korIme),
                }, "Authentication type");
                state = new AuthenticationState(new ClaimsPrincipal(identity));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}