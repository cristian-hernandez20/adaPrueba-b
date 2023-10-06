using Microsoft.AspNetCore.Mvc;
using adaPrueba_b.Services.ShoppingServices;
using adaPrueba_b.Dtos;

namespace adaPrueba_b.Controllers.v1
{
    [Route("api")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {

        private readonly IShoppingServices _servicesShopping;

        public ShoppingController(IShoppingServices ShoppingServices)
        {
            _servicesShopping = ShoppingServices;
        }

        [HttpPost("register-shopping")]
        public async Task<ActionResult<ServiceResponse<ShoppingRegisterDtos>>> SaveShopping(ShoppingRegisterDtos shopping)
        {
            return await _servicesShopping.SaveShopping(shopping);
        }
        [HttpPost("register-shopping-masive")]
        public async Task<ActionResult<ServiceResponse<List<ShoppingRegisterDtos>>>> SaveShoppingMasive(List<ShoppingRegisterDtos> shopping)
        {
            return await _servicesShopping.SaveShoppingMasive(shopping);
        }
        [HttpGet("get-shoppings")]
        public async Task<ActionResult<ServiceResponse<List<Shopping>>>> GetShoppings()
        {
            return await _servicesShopping.GetShoppings();
        }
    }
}