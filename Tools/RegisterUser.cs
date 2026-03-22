using BlazorApp.Connection;
using BlazorApp.Models;
using BlazorApp.Security;

namespace BlazorApp.Tools
{
    public static class RegisterUser
    {
        public static async Task Run(AppDbContext db)
        {
            var argon = new ArgonService();

            string defaultPassword = "Admin#123!";
            var (hash, salt) = argon.GenerateHashAndSalt(defaultPassword);

            var newUsr = new Usr { Hashy = hash, Salty = salt };
            db.Usrs.Add(newUsr);
            await db.SaveChangesAsync(); // Required to obtain user id.

            var employee = new Employee
            {
                Dni             = "72342234",
                UsrId           = newUsr.Id,
                Names           = "Elliot Rami",
                FatherLastname  = "Alderson",
                MotherLastname  = "Malek",
                Genre           = "M",
                Birthday        = new DateOnly(1986, 09, 17),
                Email           = "exampleusr@gmail.com",
                SecondEmail     = null,
                PhoneNumber     = "999888777",
                SecondPhoneNumber = null,
                TypeHire        = "CAS",
                HireDate        = new DateOnly(2015, 03, 01),
                Nationality     = "Peruana",
                CreatedAt       = DateOnly.FromDateTime(DateTime.Today),
                UpdatedAt       = DateOnly.FromDateTime(DateTime.Today)
            };

            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            // Overview
            Console.WriteLine("=== Usuario y empleado registrados ===");
            Console.WriteLine($"Usr ID    : {newUsr.Id}");
            Console.WriteLine($"Hash      : {hash}");
            Console.WriteLine($"Salt      : {salt}");
            Console.WriteLine($"Password  : {defaultPassword}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"DNI       : {employee.Dni}");
            Console.WriteLine($"Nombre    : {employee.Names} {employee.FatherLastname} {employee.MotherLastname}");
            Console.WriteLine($"Género    : {employee.Genre}");
            Console.WriteLine($"Nacimiento: {employee.Birthday}");
            Console.WriteLine($"Email     : {employee.Email}");
            Console.WriteLine($"Teléfono  : {employee.PhoneNumber}");
            Console.WriteLine($"Tipo de contrato : {employee.TypeHire}");
            Console.WriteLine($"Ingreso   : {employee.HireDate}");
            Console.WriteLine($"Creado    : {employee.CreatedAt}");
        }
    }
}
