using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorApp.Security
{
    public class SessionService
    {
        private readonly ProtectedSessionStorage _storage;
        private readonly JwtService _jwt;

        public SessionService(ProtectedSessionStorage storage, JwtService jwt)
        {
            _storage = storage;
            _jwt     = jwt;
        }

        public async Task SetSessionAsync(string token)
        {
            await _storage.SetAsync("jwt_token", token);
        }

        public async Task<ClaimsPrincipal?> GetUserAsync()
        {
            try
            {
                var result = await _storage.GetAsync<string>("jwt_token");
                if (!result.Success || string.IsNullOrEmpty(result.Value))
                    return null;

                return _jwt.ValidateToken(result.Value);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var user = await GetUserAsync();
            return user != null;
        }

        public async Task<string?> GetClaimAsync(string type)
        {
            var user = await GetUserAsync();
            return user?.FindFirst(type)?.Value;
        }

        public async Task ClearSessionAsync()
        {
            await _storage.DeleteAsync("jwt_token");
        }
    }
}