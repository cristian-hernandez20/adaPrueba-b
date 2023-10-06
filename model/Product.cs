namespace adaPrueba_b.Models
{
    public class Product
    {
        [Key]
        public Guid id { get; set; }

        [MaxLength(50)]
        public string name { get; set; } = string.Empty;

        [Column(TypeName = "numeric(3, 0)")]
        public int quantity { get; set; }

        [MaxLength(100)]
        public string descript { get; set; } = string.Empty;

        [Column(TypeName = "numeric(15, 0)")]
        public long price { get; set; }

        public string image { get; set; } = string.Empty;
    }
}