namespace adaPrueba_b.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role { id = 1, roleName = "Administrador" },
                    new Role { id = 2, roleName = "Usuario General" }
                    );

            modelBuilder.Entity<Product>().HasData(
                    new Product { id = Guid.NewGuid(), name = "Audifonos", quantity = 10, descript = "Audifonos Sony MX1000, de muy buena calidad", price = 1000000, image = "" },
                    new Product { id = Guid.NewGuid(), name = "Estuche audifono", quantity = 10, descript = "Estuche para Audifonos Sony MX1000, de muy buena calidad", price = 20000, image = "" }
                    );
        }
        public required DbSet<Product> Product { get; set; }
        public required DbSet<Shopping> Shopping { get; set; }
        public required DbSet<Role> Role { get; set; }
        public required DbSet<User> User { get; set; }
    }
}