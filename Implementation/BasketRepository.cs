using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using project.Data;
using project.Entities;
using project.Interface;

using System.Text.Json;

namespace project.Implementation
{
    public class BasketRepository : IBasketRepository
    {
        
        private readonly StoreDbContext dbContext;
        private readonly IMapper mapper;

        public BasketRepository(StoreDbContext dbContext,IMapper mapper)
        {

            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<bool> deleteBasketAsync(string basketId)
        {
            var items = dbContext.BasketItems.FirstOrDefault(x => x.CustomerBasketId==basketId);
            if(items != null) 
            dbContext.Remove(items);
            dbContext.SaveChanges();
         

            return true;


        }
      
        public async Task<CustomerBasket> getBasketAsync(string basketId)
        {
            var data = await dbContext.CustomerBasket.Include(x => x.items).FirstOrDefaultAsync();

            return data;
        }
      

        public async Task<CustomerBasket> updateBasketAsync(CustomerBasket customerBasket)
        {
            try
            {
                var result = await dbContext.CustomerBasket.AsNoTracking().Include(s => s.items).FirstOrDefaultAsync(s => s.Id == customerBasket.Id);
                if (result == null)
                {
                    await dbContext.CustomerBasket.AddAsync(customerBasket);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    foreach(var item in customerBasket.items)
                    {
                        var res = await dbContext.BasketItems.FirstOrDefaultAsync(s => s.itemId == item.itemId && s.CustomerBasketId==customerBasket.Id);
                        if(res==null)

                        {
                         await dbContext.BasketItems.AddAsync(item);
                            await dbContext.SaveChangesAsync();
                        }
                        else
                        {
                           
                            dbContext.Update(item);
                            dbContext.SaveChanges();

                        }

                    }
                   
                }
               /* await dbContext.BasketItems.AddRangeAsync(customerBasket.items);
                await dbContext.SaveChangesAsync();*/
            }
            catch (Exception ex)
            {

                throw;
            }

            return customerBasket;
                 

        }
    }
}
