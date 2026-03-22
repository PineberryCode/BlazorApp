using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [Column("dni")]
        public string Dni { get; set; } = string.Empty;

        [Column("usr_id")]
        public int? UsrId { get; set; }

        [Column("names")]
        public string Names { get; set; } = string.Empty;

        [Column("father_lastname")]
        public string FatherLastname { get; set; } = string.Empty;

        [Column("mother_lastname")]
        public string MotherLastname { get; set; } = string.Empty;

        [Column("genre")]
        public string Genre { get; set; } = string.Empty;

        [Column("birthday")]
        public DateOnly Birthday { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("second_email")]
        public string? SecondEmail { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("second_phone_number")]
        public string? SecondPhoneNumber { get; set; }

        [Column("type_hire")]
        public string TypeHire { get; set; } = string.Empty;

        [Column("hire_date")]
        public DateOnly HireDate { get; set; }

        [Column("nationality")]
        public string? Nationality { get; set; }

        [Column("created_at")]
        public DateOnly CreatedAt { get; set; }

        [Column("updated_at")]
        public DateOnly UpdatedAt { get; set; }

        [ForeignKey("UsrId")]
        public Usr? Usr { get; set; }
    }
}