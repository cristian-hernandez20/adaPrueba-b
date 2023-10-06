using adaPrueba_b.Data;
using adaPrueba_b.Dtos;
using adaPrueba_b.Midddlewares;
namespace adaPrueba_b.Services.ProductServices
{

    public interface IProductServices
    {
        Task<ServiceResponse<Product>> SaveProduct(Product product);
        Task<ServiceResponse<Product>> EditProduct(Product product);
        Task<ServiceResponse<bool>> DeleteProduct(Guid id);
        Task<ServiceResponse<List<Product>>> GetProducts();

    }
    public class ProductServices : IProductServices
    {
        private readonly DataContext _context;
        private readonly IAutorizacion _autorizacion;

        public ProductServices(DataContext context, IAutorizacion autorizacion)
        {
            _context = context;
            _autorizacion = autorizacion;
        }
        public async Task<ServiceResponse<Product>> SaveProduct(Product product)
        {
            ServiceResponse<Product> response = new();
            try
            {
                var dbProduct = await _context.Product.FirstOrDefaultAsync(c => c.name.Equals(product.name));
                if (dbProduct != null)
                {
                    response.Success = false;
                    response.Message = $"El producto {product.name} ya se encuentra registrado";
                    return response;
                }
                _context.Product.Add(product);
                response.Success = true;
                await _context.SaveChangesAsync();
                response.Message = $"El producto {product.name} se creo correctamente";
                response.Data = product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al guardar producto";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }

        public async Task<ServiceResponse<Product>> EditProduct(Product product)
        {
            ServiceResponse<Product> response = new();
            try
            {
                var dbProduct = await _context.Product.FirstOrDefaultAsync(c => c.id.Equals(product.id));
                if (dbProduct == null)
                {
                    response.Success = false;
                    response.Message = $"El producto con el código {product.name} no existe";
                    return response;
                }
                _context.Entry(dbProduct).State = EntityState.Detached;
                dbProduct = product;
                _context.Update(dbProduct);

                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = $"El producto {product.name} se actualizo correctamente";
                response.Data = product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al modificar producto.";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(Guid id)
        {
            ServiceResponse<bool> response = new();
            try
            {
                var dbProduct = await _context.Product.FirstOrDefaultAsync(c => c.id.Equals(id));
                if (dbProduct == null)
                {
                    response.Success = false;
                    response.Message = $"El producto {id} no se existe";
                    return response;
                }
                _context.Product.Remove(dbProduct);
                await _context.SaveChangesAsync();
                response.Message = $"El producto {dbProduct.name} se elimino correctamente";
                response.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = $"Ocurrió un error al retirar producto {id}.";
                response.Error = $"Exception -> {ex}";
            }
            return response;
        }
        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            ServiceResponse<List<Product>> response = new();
            try
            {
                var dbProduct = await _context.Product.ToListAsync();
                if (dbProduct == null || !dbProduct.Any())
                {
                    response.Success = false;
                    response.Message = "No existen productos";
                    return response;
                }
                response.Message = "Product";
                response.Success = true;
                response.Data = dbProduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception -> {ex}");
                response.Success = false;
                response.Message = "Ocurrió un error al obtener los productos";
                response.Error = $"Exception -> {ex}";
            }
            return response;

        }
    }
}
