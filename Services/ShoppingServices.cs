using adaPrueba_b.Data;
using adaPrueba_b.Dtos;
using adaPrueba_b.Midddlewares;
namespace adaPrueba_b.Services.ShoppingServices
{
    public interface IShoppingServices
    {
        Task<ServiceResponse<ShoppingRegisterDtos>> SaveShopping(ShoppingRegisterDtos product);
        Task<ServiceResponse<List<ShoppingRegisterDtos>>> SaveShoppingMasive(List<ShoppingRegisterDtos> product);
        Task<ServiceResponse<List<Shopping>>> GetShoppings();
    }
    public class ShoppingServices : IShoppingServices
    {
        private readonly DataContext _context;
        public ShoppingServices(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<ShoppingRegisterDtos>>> SaveShoppingMasive(List<ShoppingRegisterDtos> product)
        {
            ServiceResponse<List<ShoppingRegisterDtos>> response = new();
            try
            {
                var dbProductList = new List<Shopping>();
                foreach (var item in product)
                {
                    var dbProduct = new Shopping();
                    dbProduct.id = Guid.NewGuid();
                    dbProduct.id_product = item.id_product;
                    dbProduct.id_user = item.id_user;
                    var dbProductSelect = await _context.Product.FirstOrDefaultAsync(c => c.id.Equals(item.id_product));
                    if (dbProductSelect != null)
                    {
                        _context.Entry(dbProductSelect).State = EntityState.Detached;
                        dbProductSelect.quantity = dbProductSelect.quantity - 1;
                        _context.Update(dbProductSelect);
                    }


                    await _context.SaveChangesAsync();
                    dbProductList.Add(dbProduct);
                }

                _context.Shopping.AddRange(dbProductList);
                response.Success = true;
                await _context.SaveChangesAsync();
                response.Message = $"La compra registrada correctamente";
                response.Data = product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al guardar compra";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }
        public async Task<ServiceResponse<ShoppingRegisterDtos>> SaveShopping(ShoppingRegisterDtos product)
        {
            ServiceResponse<ShoppingRegisterDtos> response = new();
            try
            {
                var dbProduct = new Shopping();
                dbProduct.id = Guid.NewGuid();
                dbProduct.id_product = product.id_product;
                dbProduct.id_user = product.id_user;
                _context.Shopping.Add(dbProduct);
                response.Success = true;
                await _context.SaveChangesAsync();
                response.Message = $"La compra registrada correctamente";
                response.Data = product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al guardar compra";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<Shopping>>> GetShoppings()
        {
            ServiceResponse<List<Shopping>> response = new();
            try
            {
                var dbShopping = await _context.Shopping.Include(x => x.Product).Include(x => x.User).ToListAsync();
                if (dbShopping == null || !dbShopping.Any())
                {
                    response.Success = false;
                    response.Message = "No existen compras";
                    return response;
                }
                response.Message = "Shopping";
                response.Success = true;
                response.Data = dbShopping;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al obtener los compras";
                response.Error = $"Exception -> {ex}";
            }
            return response;

        }
    }
}
