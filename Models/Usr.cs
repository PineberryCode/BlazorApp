using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    [Table("usr")]
    public class Usr
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("hashy")]
        public string Hashy { get; set; } = string.Empty;

        [Column("salty")]
        public string Salty { get; set; } = string.Empty;
    }
}