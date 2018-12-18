using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LNPizzaLNPizza.DataAccess;
namespace LNPizza.DataAccess
{
    public partial class PizzaDB2Context : DbContext
    {
        public PizzaDB2Context()
        {
        }

        public PizzaDB2Context(DbContextOptions<PizzaDB2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<OrderedPizzas> OrderedPizzas { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<PizzaIngredients> PizzaIngredients { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreIngredients> StoreIngredients { get; set; }
        public virtual DbSet<UserAddress> UserAddress { get; set; }
        public virtual DbSet<UserTbl> UserTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Console.WriteLine("setup options builder");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<OrderedPizzas>(entity =>
            {
                entity.HasKey(e => e.OrderId1)
                    .HasName("PK__OrderedP__59B35644C2CF2C64");

                entity.Property(e => e.OrderId1)
                    .HasColumnName("_orderId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.OrderedPizzasIdNavigation)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaIDOrderDetails");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderedPizzas)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderID");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrderedPizzasPizza)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPizzaID");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StoreAddressId).HasColumnName("StoreAddressID");

                entity.Property(e => e.UserOrderAddressId).HasColumnName("UserOrderAddressID");

                entity.HasOne(d => d.StoreAddress)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreAddressID");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Costs).HasColumnType("money");

                entity.Property(e => e.PizzaName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<PizzaIngredients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasMaxLength(108);

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaIngredients)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaID");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreAddressId)
                    .HasName("PK__Store__05A960CE40D35254");

                entity.Property(e => e.StoreAddressId).HasColumnName("StoreAddressID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryAbrev)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ProvidenceState)
                    .IsRequired()
                    .HasMaxLength(108);

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreIngredients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StoreIngredientsAddressId).HasColumnName("StoreIngredientsAddressID");

                entity.HasOne(d => d.StoreIngredientsAddress)
                    .WithMany(p => p.StoreIngredients)
                    .HasForeignKey(d => d.StoreIngredientsAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreIngredientsAddressID");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.Property(e => e.UserAddressId)
                    .HasColumnName("UserAddressID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryAbrev)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ProvidenceState)
                    .IsRequired()
                    .HasMaxLength(108);

                entity.HasOne(d => d.UserAddressNavigation)
                    .WithOne(p => p.UserAddress)
                    .HasForeignKey<UserAddress>(d => d.UserAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddressID");
            });

            modelBuilder.Entity<UserTbl>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserTBL__1788CCACCA2C5331");

                entity.ToTable("UserTBL");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(108);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(108);
            });
        }
    }
}
