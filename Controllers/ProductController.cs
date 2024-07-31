using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.DTO;
using project.Entities;
using project.Errors;
using project.Helper;
using project.Interface;
using project.Specification;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductType> productType;
        private readonly IGenericRepository<ProductBrand> productBrand;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> productRepo,IGenericRepository<ProductType> productType,IGenericRepository<ProductBrand> productBrand,
            IMapper mapper)
        {
            
            this.productRepo = productRepo;
            this.productType = productType;
            this.productBrand = productBrand;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProduct([FromQuery]ProductSpecificParams productSpecificParams) 
        {

            var spec = new ProductWithTypeAndBrandSpecification(productSpecificParams);
            var countSpec=new ProductWithFilterCountSpecification(productSpecificParams);
            var totalItems=await productRepo.CountAsync(countSpec);
            var result = await productRepo.ListAsync(spec);

            List<ProductDto> products = new List<ProductDto>();
            /*  foreach (var item in result)
                {
                    products.Add(new ProductDto {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        PictureUrl = item.PictureUrl,
                        productbrand = item.productbrand.Name,
                        producttype = item.producttype.Name

                    });
                }  */
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(result);
            return Ok(new Pagination<ProductDto>(productSpecificParams.PageIndex,productSpecificParams.PageSize,totalItems,data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var result =await productRepo.getEntityWithSpecification(spec);
            /* var answer=new ProductDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Price = result.Price,
                    PictureUrl = result.PictureUrl,
                    productbrand = result.productbrand.Name,
                    producttype=result.producttype.Name

                };*/
            if(result==null)
            {
                return NotFound(new ApiResponse(404));
            }
            return mapper.Map<Product, ProductDto>(result);
        }
        [HttpGet("brand")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrand()
        {
            var result = await productBrand.listAllAsync();
            return Ok(result);
        }
        [HttpGet("type")]
        public async Task<ActionResult<List<ProductBrand>>> GetTypes()
        {
            var result = await productType.listAllAsync();
            return Ok(result);
        }


    }
}
