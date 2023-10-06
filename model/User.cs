namespace adaPrueba_b.Models
{

    [Index(nameof(nameUser), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid id { get; set; }

        [MaxLength(50)]
        public required string name { get; set; }

        [MaxLength(50)]
        public required string lastName { get; set; }

        [MaxLength(100)]
        public required string addres { get; set; }

        [Column(TypeName = "numeric(10, 0)")]
        public long phone { get; set; }

        [Column(TypeName = "numeric(15, 0)")]
        public long identification { get; set; }

        [MaxLength(10)]
        public required string nameUser { get; set; }

        [Column(TypeName = "numeric(1, 0)")]
        public int rol { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; } = { 0 };
        [Required]
        public byte[] PasswordSalt { get; set; } = { 0 };
    }
}