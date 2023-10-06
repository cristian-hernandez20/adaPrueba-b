namespace adaPrueba_b.Models
{
    public class Shopping
    {
        [Key]
        public Guid id { get; set; }

        public Guid id_user { get; set; }
        [ForeignKey("id_user")]
        public User? User { get; set; }

        public Guid id_product { get; set; }
        [ForeignKey("id_product")]
        public Product? Product { get; set; }

        public DateTime dateCreated { get; set; } = DateTime.UtcNow;
    }
}