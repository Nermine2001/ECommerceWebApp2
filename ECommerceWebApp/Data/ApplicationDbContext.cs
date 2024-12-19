using ECommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<CartItem> CartItems { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Contact> Contacts { get; set; }
	public DbSet<Testimonial> Testimonials { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Product Model
		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(p => p.Id);
			entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
			entity.Property(p => p.Description).HasMaxLength(500);
			entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
			entity.Property(p => p.ImageURL).HasMaxLength(200);
		});

		// Cart Model
		modelBuilder.Entity<Cart>(entity =>
		{
			entity.HasKey(c => c.Id);
			entity.HasOne(c => c.User)
				  .WithMany(u => u.Carts)
				  .HasForeignKey(c => c.UserId)
				  .OnDelete(DeleteBehavior.Cascade);
		});

		// CartItem Model
		modelBuilder.Entity<CartItem>(entity =>
		{
			entity.HasKey(ci => ci.Id);

			// Relation avec Cart
			entity.HasOne(ci => ci.Cart)
				  .WithMany(c => c.Items)
				  .HasForeignKey(ci => ci.CartId)
				  .OnDelete(DeleteBehavior.Cascade); // Supprime les items si le panier est supprimé.

			// Relation avec Product
			entity.HasOne(ci => ci.Product)
				  .WithMany()
				  .HasForeignKey(ci => ci.ProductId)
				  .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression d'un produit encore utilisé.

			entity.Property(ci => ci.Quantity).IsRequired();
		});


		// Order Model
		modelBuilder.Entity<Order>(entity =>
		{
			entity.HasKey(o => o.Id);
			entity.Property(o => o.ShippingAddress).HasMaxLength(200);
			entity.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
			entity.HasOne(o => o.User)
				  .WithMany(u => u.Orders)
				  .HasForeignKey(o => o.UserId)
				  .OnDelete(DeleteBehavior.Cascade);
			entity.HasMany(o => o.OrderItems)
				  .WithOne(oi => oi.Order)
				  .HasForeignKey(oi => oi.OrderId);
		});

		// OrderItem Model
		modelBuilder.Entity<OrderItem>(entity =>
		{
			entity.HasKey(oi => oi.Id);
			entity.HasOne(oi => oi.Product)
				  .WithMany()
				  .HasForeignKey(oi => oi.ProductId);
			entity.Property(oi => oi.Quantity).IsRequired();
			entity.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
		});

		// User Model
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(u => u.Id);
			entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
			entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
			entity.Property(u => u.Password).IsRequired().HasMaxLength(200); // Ensure passwords are hashed.
			entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
		});
	}
}
