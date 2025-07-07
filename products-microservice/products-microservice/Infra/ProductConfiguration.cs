using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace products_microservice.Infra;

public class ProductConfiguration
{
    public void Configure(EntityTypeBuilder<ProductDbModel> builder)
    {
        builder.HasKey(p => p.Id);
    }
}