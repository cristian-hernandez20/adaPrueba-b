using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Claims;
using adaPrueba_b.Data;
using adaPrueba_b.Dtos;

namespace adaPrueba_b.Midddlewares
{


    public interface IAutorizacion
    {
        Task<ServiceResponse<UserDtos>> SingIn(string nameUser, string password);
        int GetRoleId();
    }
    public class Autorizacion : IAutorizacion
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Autorizacion(DataContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId() => _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        public int GetRoleId()
        {
            var roleClaim = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (roleClaim != null) { return int.Parse(roleClaim); }
            throw new InvalidOperationException("No se encontró un valor válido para el rol.");
        }

        public async Task<ServiceResponse<UserDtos>> SingIn(string nameUser, string password)
        {
            ServiceResponse<UserDtos> response = new();
            try
            {
                var dbUser = await _context.User.FirstOrDefaultAsync(x => x.nameUser.Equals(nameUser));

                if (dbUser == null) { response.Success = false; response.Message = "El usuario no existe porfavor registrate"; response.Error = "01"; }
                else if (!VerifyPasswordHash(password, dbUser.PasswordHash, dbUser.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Contraseña es incorrecta";
                    return response;
                }
                else
                {
                    response.Message = "autorización success";
                    response.Success = true;
                    response.Data = new UserDtos()
                    {
                        id = dbUser.id,
                        rol = dbUser.rol,
                        name = dbUser.name,
                        addres = dbUser.addres,
                        lastName = dbUser.lastName,
                        nameUser = dbUser.nameUser,
                        token = await CreateToken(dbUser)
                    };
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = $"Ocurrió un error al obtener la autenticación";
                response.Error = $"Exception -> {ex}";
            }

            return response;
        }
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier,  user.id.ToString()),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Role, user.rol.ToString()),
            };

            var tokenValue = _configuration.GetSection("AppSettings:Token").Value ?? "no_token";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenValue));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(jwt);
        }
    }
}