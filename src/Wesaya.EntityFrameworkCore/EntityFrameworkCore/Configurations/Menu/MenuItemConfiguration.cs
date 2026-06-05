using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(MenuConsts.MaxItemNameLength);

        builder.Property(x => x.Description)
            .HasMaxLength(MenuConsts.MaxItemDescriptionLength);

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

            extraItemBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(MenuConsts.MaxExtraItemNameLength);

            extraItemBuilder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            extraItemBuilder.WithOwner()
                .HasForeignKey("MenuItemId");

            extraItemBuilder.HasIndex("MenuItemId", nameof(ExtraItem.Name))
                .IsUnique();
        });

        builder.Navigation(x => x.ExtraItems)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<MenuCategory>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.Name);
    }
}
