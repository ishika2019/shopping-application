using AutoMapper;
using project.DTO;
using project.Entities;

namespace project.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(source.PictureUrl!=null)
            {
                return configuration["ApiUrl"]+source.PictureUrl;
            }
            return null;
        }
    }
}
