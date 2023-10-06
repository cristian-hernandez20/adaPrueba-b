using System.Security.Cryptography;
using adaPrueba_b.Data;
using adaPrueba_b.Dtos;
namespace adaPrueba_b.Services.UserServices
{

    public interface IUserServices
    {
        Task<ServiceResponse<UserRegisterDtos>> SaveUser(UserRegisterDtos user);
        Task<ServiceResponse<User>> EditUser(User user);
        Task<ServiceResponse<User>> DeleteUser(Guid id);
        Task<ServiceResponse<User>> GetUser(int nameUser);
        Task<ServiceResponse<List<User>>> GetUsers();

    }
    public class UserServices : IUserServices
    {
        private readonly DataContext _context;

        public UserServices(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<UserRegisterDtos>> SaveUser(UserRegisterDtos user)
        {
            ServiceResponse<UserRegisterDtos> response = new();
            try
            {
                var dbMunic = await _context.User.FirstOrDefaultAsync(c => c.nameUser.Equals(user.nameUser));
                if (dbMunic != null)
                {
                    response.Success = false;
                    response.Message = $"El usuario {user.nameUser} ya se encuentra registrado";
                    return response;
                }
                User userNew = new()
                {
                    id = Guid.NewGuid(),
                    rol = 2,
                    name = user.name,
                    phone = user.phone,
                    addres = user.addres,
                    nameUser = user.nameUser,
                    lastName = user.lastName,
                    identification = user.identification,

                };
                CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
                userNew.PasswordHash = passwordHash;
                userNew.PasswordSalt = passwordSalt;
                _context.User.Add(userNew);
                response.Success = true;
                await _context.SaveChangesAsync();
                response.Message = $"El usuario {user.nameUser} se creo correctamente";
                response.Data = user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al guardar usuario";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        public async Task<ServiceResponse<User>> EditUser(User user)
        {
            ServiceResponse<User> response = new();
            try
            {
                var dbMunic = await _context.User.FirstOrDefaultAsync(c => c.id.Equals(user.id));
                if (dbMunic == null)
                {
                    response.Success = false;
                    response.Message = $"El usuario con el código {user.nameUser} no existe";
                    return response;
                }
                _context.Entry(dbMunic).State = EntityState.Detached;
                dbMunic = user;
                _context.Update(user);

                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = $"El usuario {user.nameUser} se actualizo correctamente";
                response.Data = user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al modificar usuario.";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }

        public async Task<ServiceResponse<User>> DeleteUser(Guid id)
        {
            ServiceResponse<User> response = new();
            try
            {
                var dbMunic = await _context.User.FirstOrDefaultAsync(c => c.id.Equals(id));
                if (dbMunic == null)
                {
                    response.Success = false;
                    response.Message = $"El usuario {id} no se existe";
                    return response;
                }
                _context.User.Remove(dbMunic);
                await _context.SaveChangesAsync();
                response.Message = $"El usuario {id} se elimino correctamente";
                response.Success = true;
                response.Data = dbMunic;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = $"Ocurrió un error al retirar usuario {id}.";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }
        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            ServiceResponse<List<User>> response = new();
            try
            {
                var dbMunic = await _context.User.ToListAsync();
                if (dbMunic == null || !dbMunic.Any())
                {
                    response.Success = false;
                    response.Message = "No existen usuario";
                    return response;
                }
                response.Message = "User";
                response.Success = true;
                response.Data = dbMunic;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al obtener los usuario";
                response.Error = $"Exception -> {ex}";
            }
            return response;

        }

        public async Task<ServiceResponse<User>> GetUser(int nameUser)
        {
            ServiceResponse<User> response = new();
            try
            {
                var dbMunic = await _context.User.FirstOrDefaultAsync(c => c.nameUser.Equals(nameUser));
                if (dbMunic == null)
                {
                    response.Success = false;
                    response.Message = $"No se encontro el usuario {nameUser}";
                    return response;
                }
                response.Message = "User";
                response.Success = true;
                response.Data = dbMunic;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al obtener el usuario.";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }
    }
}
