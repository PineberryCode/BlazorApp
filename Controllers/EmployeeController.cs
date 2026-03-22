using BlazorApp.Connection;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
    public class EmployeeController
    {
        private readonly AppDbContext _db;

        public EmployeeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> UpdateOptionalFields(
            string dni,
            string email,
            string? secondEmail,
            string phoneNumber,
            string? secondPhoneNumber)
        {
            var employee = await _db.Employees
                .FirstOrDefaultAsync(e => e.Dni == dni);

            if (employee == null) return false;

            employee.Email = email;
            employee.PhoneNumber = phoneNumber;
            employee.SecondEmail       = secondEmail;
            employee.SecondPhoneNumber = secondPhoneNumber;
            employee.UpdatedAt         = DateOnly.FromDateTime(DateTime.Today);

            await _db.SaveChangesAsync();
            return true;
        }
    }
}