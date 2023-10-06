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
        }
        public required DbSet<Role> Role { get; set; }
        public required DbSet<User> User { get; set; }
    }
}