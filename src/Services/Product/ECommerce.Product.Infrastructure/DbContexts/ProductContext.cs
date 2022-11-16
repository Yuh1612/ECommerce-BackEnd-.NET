#nullable disable

using ECommerce.Products.Domain.Entities;
using ECommerce.Shared.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Products.Infrastructure.DbContexts
{
    public partial class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<ProductOptions> ProductOptions { get; set; }
        public virtual DbSet<Search> Search { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShopProducts> ShopProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("Option");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((1000))");

                entity.Property(e => e.Slug)
                    .HasMaxLength(50)
                    .HasColumnName("slug")
                    .IsFixedLength();

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Shop");
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.ToTable("ProductCategories");

                entity.HasKey(e => new { e.ProductId, e.CategoryId });

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategories_Category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategories_Product");
            });

            modelBuilder.Entity<ProductOptions>(entity =>
            {
                entity.ToTable("ProductOptions");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.ProductOptions)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOptions_Option");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOptions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOptions_Product");
            });

            modelBuilder.Entity<Search>(entity =>
            {
                entity.ToTable("Search");

                entity.HasKey(e => e.UserId)
                    .HasName("PK__Search__1788CC4C50F534DA");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Keyword).IsRequired();
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ShopProducts>(entity =>
            {
                entity.ToTable("ShopProducts");

                entity.HasNoKey();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShopProducts_Product");

                entity.HasOne(d => d.Shop)
                    .WithMany()
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShopProducts_Shop");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}