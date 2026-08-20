using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.API.Data.Models;

namespace Storefront.API.Data.Entity_Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasIndex(x => new { x.Id, x.SKU })
                .IsUnique();
        }
    }
}
