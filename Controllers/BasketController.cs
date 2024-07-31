using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Entities;
using project.Interface;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> getBasketbyId(string id)
        {
            var basket = await basketRepository.getBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> updateBasket(CustomerBasket customerBasket)
        {
            var basket = await basketRepository.updateBasketAsync(customerBasket);
            return Ok(basket);

        }
        
        [HttpDelete]

        public async Task<bool> deleteBasket(string id)
        {
          return  await basketRepository.deleteBasketAsync(id);

        }

       
        
    }
}
