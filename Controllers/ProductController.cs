using Microsoft.AspNetCore.Mvc;
using adaPrueba_b.Services.ProductServices;
using adaPrueba_b.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace adaPrueba_b.Controllers.v1
{
    [Route("api")]
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