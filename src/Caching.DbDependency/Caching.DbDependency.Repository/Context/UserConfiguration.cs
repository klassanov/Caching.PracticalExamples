using Caching.DbDependency.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caching.DbDependency.Repository.Context
{
    public class UserConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();
                
            builder.Property(p => p.Price)
                .IsRequired();

            builder.HasData(
                new Product
                {
                    Id = new Guid("ed1dd774-14a8-45cd-99bb-5567dba9aad7"),
                    Name = "Product 1",
                    Price = 10.0m
                },
                new Product
                {
                    Id = new Guid("a42e98cc-ae38-4fa9-abb9-350ccf654f93"),
                    Name = "Product 2",
                    Price = 20.0m
                },
                new Product
                {
                    Id = new Guid("3db7e087-bec7-4605-8c73-89edd88eb693"),
                    Name = "Product 3",
                    Price = 30.0m
                }
            );
        }
    }
}
