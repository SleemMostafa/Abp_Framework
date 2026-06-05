using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wesaya.Menu;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Wesaya.EntityFrameworkCore.Configurations.Menu;

public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
{
    public void Configure(EntityTypeBuilder<MenuCategory> builder)
    {
        builder.ToTable(WesayaConsts.DbTablePrefix + "MenuCategories", WesayaConsts.DbSchema);

        builder.ConfigureByConvention();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(MenuConsts.MaxCategoryNameLength);

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.DisplayOrder);
    }
}
