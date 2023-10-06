namespace adaPrueba_b.Dtos
{
    public class UserDtos
    {
        public required Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string addres { get; set; } = string.Empty;
        public long phone { get; set; }
        public long identification { get; set; }
        public string nameUser { get; set; } = string.Empty;
        public int rol { get; set; }
        public string token { get; set; } = string.Empty;
    }

    public class UserLogin
    {
        public string nameUser { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }

    public class UserRegisterDtos
    {
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string addres { get; set; } = string.Empty;
        public long phone { get; set; }
        public long identification { get; set; }
        public string nameUser { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int rol { get; set; }
    }
}