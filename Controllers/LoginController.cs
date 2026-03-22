using BlazorApp.Connection;
using BlazorApp.Models;
using BlazorApp.Security;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
    public class LoginController
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;
        private readonly SessionService _session;
        public LoginController(AppDbContext db, JwtService jwt, SessionService session)
        {
            _db = db;
            _jwt = jwt;
            _session = session;
        }

        public async Task<bool> SignIn(string dni, string password)
        {
            var employee = await _db.Employees
                .Include(e => e.Usr)
                .FirstOrDefaultAsync(e => e.Dni == dni);

            if (employee == null || employee.Usr == null)
                return false;

            bool isValid = Argon2.Verify(employee.Usr.Hashy, password);
            if (!isValid) return false;

            string token = _jwt.GenerateToken(employee);
            await _session.SetSessionAsync(token);

            return true;
        }

        public async Task SignOut() => await _session.ClearSessionAsync();
    }
}
