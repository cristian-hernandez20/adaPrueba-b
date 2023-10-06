using System.Security.Cryptography;
using adaPrueba_b.Data;
using adaPrueba_b.Dtos;
using adaPrueba_b.Midddlewares;
namespace adaPrueba_b.Services.UserServices
{

    public interface IUserServices
    {
        Task<ServiceResponse<UserRegisterDtos>> SaveUser(UserRegisterDtos user);
        Task<ServiceResponse<User>> EditUser(User user);
        Task<ServiceResponse<bool>> DeleteUser(Guid id);
        Task<ServiceResponse<List<User>>> GetUsers();

    }
    public class UserServices : IUserServices
    {
        private readonly DataContext _context;
        private readonly IAutorizacion _autorizacion;

        public UserServices(DataContext context, IAutorizacion autorizacion)
        {
            _context = context;
            _autorizacion = autorizacion;
        }
        public async Task<ServiceResponse<UserRegisterDtos>> SaveUser(UserRegisterDtos user)
        {
            ServiceResponse<UserRegisterDtos> response = new();
            try
            {
                var dbUser = await _context.User.FirstOrDefaultAsync(c => c.nameUser.Equals(user.nameUser));
                if (dbUser != null)
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
                var dbUser = await _context.User.FirstOrDefaultAsync(c => c.id.Equals(user.id));
                if (dbUser == null)
                {
                    response.Success = false;
                    response.Message = $"El usuario con el código {user.nameUser} no existe";
                    return response;
                }
                _context.Entry(dbUser).State = EntityState.Detached;
                dbUser.id = user.id;
                dbUser.name = user.name;
                dbUser.lastName = user.lastName;
                dbUser.addres = user.addres;
                dbUser.phone = user.phone;
                dbUser.identification = user.identification;
                dbUser.nameUser = user.nameUser;
                _context.Update(dbUser);

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

        public async Task<ServiceResponse<bool>> DeleteUser(Guid id)
        {
            ServiceResponse<bool> response = new();
            try
            {
                var dbUser = await _context.User.FirstOrDefaultAsync(c => c.id.Equals(id));
                if (dbUser == null)
                {
                    response.Success = false;
                    response.Message = $"El usuario {id} no se existe";
                    return response;
                }
                _context.User.Remove(dbUser);
                await _context.SaveChangesAsync();
                response.Message = $"El usuario {dbUser.nameUser} se elimino correctamente";
                response.Success = true;
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
                int role = _autorizacion.GetRoleId();

                if (role == 2)
                {
                    response.Success = false;
                    response.Message = $"No tiene autorización para realizar esta solicitud";
                    return response;
                }
                var dbUser = await _context.User.ToListAsync();
                if (dbUser == null || !dbUser.Any())
                {
                    response.Success = false;
                    response.Message = "No existen usuario";
                    return response;
                }
                response.Message = "User";
                response.Success = true;
                response.Data = dbUser;
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
    }
}
