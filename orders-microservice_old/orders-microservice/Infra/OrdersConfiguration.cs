using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace orders_microservice.Infra;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemDbModel>
{
    public void Configure(EntityTypeBuilder<OrderItemDbModel> builder)
    {
        builder.HasKey(o => o.Id);
    }
}

public class OrdersConfiguration : IEntityTypeConfiguration<OrderDbModel>
{
    public void Configure(EntityTypeBuilder<OrderDbModel> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasMany(o => o.Items);
    }
}