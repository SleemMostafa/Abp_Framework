using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wesaya.Localization;
using Wesaya.Menu;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Wesaya.EntityFrameworkCore.Configurations.Menu;

public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
{
    public void Configure(EntityTypeBuilder<MenuCategory> builder)
    {
        builder.ToTable(WesayaConsts.DbTablePrefix + "MenuCategories", WesayaConsts.DbSchema);

        builder.ConfigureByConvention();

        builder.OwnsOne(x => x.Name, nameBuilder =>
        {
            ConfigureStrongLocalizedString(
                nameBuilder,
                "Name",
                MenuConsts.MaxCategoryNameLength);

            nameBuilder.HasIndex(x => x.English);
            nameBuilder.HasIndex(x => x.Arabic);
        });

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => x.DisplayOrder);
    }

    private static void ConfigureStrongLocalizedString(
        OwnedNavigationBuilder<MenuCategory, StrongLocalizedString> builder,
        string prefix,
        int maxLength)
    {
        builder.Property(x => x.English)
            .HasColumnName(prefix + "English")
            .IsRequired()
            .HasMaxLength(maxLength);

        builder.Property(x => x.Arabic)
            .HasColumnName(prefix + "Arabic")
            .IsRequired()
            .HasMaxLength(maxLength);
    }
}
