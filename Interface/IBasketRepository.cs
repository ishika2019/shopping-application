using Microsoft.AspNetCore.Mvc;
using project.Entities;

namespace project.Interface
{
    public interface IBasketRepository
    {

        Task<CustomerBasket> getBasketAsync(string basketId);

        Task<CustomerBasket> updateBasketAsync(CustomerBasket customerBasket);

        Task<bool>  deleteBasketAsync(string basketId);

    }
}
