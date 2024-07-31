using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project.Entities;

namespace project.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(p=>p.Id).IsRequired();
           builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(b => b.productbrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(t => t.producttype).WithMany().HasForeignKey(p => p.ProductTypeId);

        }
    }
}
