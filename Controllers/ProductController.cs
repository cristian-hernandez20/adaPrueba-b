using Microsoft.AspNetCore.Mvc;
using adaPrueba_b.Services.ProductServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace adaPrueba_b.Controllers.v1
{
    [Route("api"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductServices _servicesProduct;

        public ProductController(IProductServices ProductServices)
        {
            _servicesProduct = ProductServices;
        }

        [HttpPost("register-product")]
        public async Task<ActionResult<ServiceResponse<Product>>> SaveProduct(Product product)
        {
            return await _servicesProduct.SaveProduct(product);
        }
        [HttpPut("edit-product")]
        public async Task<ActionResult<ServiceResponse<Product>>> EditProduct(Product product)
        {
            return await _servicesProduct.EditProduct(product);
        }
        [HttpGet("get-products")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            return await _servicesProduct.GetProducts();
        }
        [HttpDelete("delete-product")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct([FromQuery] Guid id)
        {
            return await _servicesProduct.DeleteProduct(id);
        }
    }
}