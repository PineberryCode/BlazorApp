using BlazorApp.Connection;
using BlazorApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
    public class NotificationController
    {
        private readonly AppDbContext _db;
        private readonly EmailService _email;

        public NotificationController(AppDbContext db, EmailService email)
        {
            _db    = db;
            _email = email;
        }

        public async Task<bool> SendByDniAsync(string dni, string subject, string body)
        {
            var employee = await _db.Employees
                .Where(e => e.Dni == dni)
                .Select(e => new { e.Email, e.Names })
                .FirstOrDefaultAsync();

            if (employee == null) return false;

            await _email.SendAsync(employee.Email, subject, body);
            return true;
        }
    }
}