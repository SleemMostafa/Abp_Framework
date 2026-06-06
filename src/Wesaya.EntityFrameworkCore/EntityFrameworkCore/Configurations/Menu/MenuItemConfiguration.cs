using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wesaya.Localization;
using Wesaya.Menu;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Wesaya.EntityFrameworkCore.Configurations.Menu;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable(WesayaConsts.DbTablePrefix + "MenuItems", WesayaConsts.DbSchema);

        builder.ConfigureByConvention();

        builder.Property(x => x.CategoryId)
            .IsRequired();

        builder.OwnsOne(x => x.Name, nameBuilder =>
        {
            ConfigureStrongLocalizedString(
                nameBuilder,
                "Name",
                MenuConsts.MaxItemNameLength);

            nameBuilder.HasIndex(x => x.English);
            nameBuilder.HasIndex(x => x.Arabic);
        });

        builder.OwnsOne(x => x.Description, descriptionBuilder =>
        {
            ConfigureOptionalLocalizedString(
                descriptionBuilder,
                "Description",
                MenuConsts.MaxItemDescriptionLength);
        });

        builder.Navigation(x => x.Description)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IsAvailable)
            .IsRequired();

        builder.Property(x => x.PreparationTimeMinutes)
            .IsRequired();

        builder.OwnsMany(x => x.ExtraItems, extraItemBuilder =>
        {
            extraItemBuilder.ToTable(WesayaConsts.DbTablePrefix + "MenuItemExtraItems", WesayaConsts.DbSchema);

            extraItemBuilder.Property<Guid>("MenuItemId");
            extraItemBuilder.Property<int>("Id");
            extraItemBuilder.HasKey("MenuItemId", "Id");

            extraItemBuilder.OwnsOne(x => x.Name, nameBuilder =>
            {
                nameBuilder.Property(x => x.English)
                    .HasColumnName("NameEnglish")
                    .IsRequired()
                    .HasMaxLength(MenuConsts.MaxExtraItemNameLength);

                nameBuilder.Property(x => x.Arabic)
                    .HasColumnName("NameArabic")
                    .IsRequired()
                    .HasMaxLength(MenuConsts.MaxExtraItemNameLength);
            });

            extraItemBuilder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            extraItemBuilder.WithOwner()
                .HasForeignKey("MenuItemId");

        });

        builder.Navigation(x => x.ExtraItems)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<MenuCategory>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CategoryId);
    }

    private static void ConfigureStrongLocalizedString(
        OwnedNavigationBuilder<MenuItem, StrongLocalizedString> builder,
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

    private static void ConfigureOptionalLocalizedString(
        OwnedNavigationBuilder<MenuItem, OptionalLocalizedString> builder,
        string prefix,
        int maxLength)
    {
        builder.Property(x => x.English)
            .HasColumnName(prefix + "English")
            .HasMaxLength(maxLength);

        builder.Property(x => x.Arabic)
            .HasColumnName(prefix + "Arabic")
            .HasMaxLength(maxLength);
    }
}
