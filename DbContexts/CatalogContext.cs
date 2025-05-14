using Backend.Entities;
using Backend.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Backend.DbContexts
{
	public class CatalogContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<SubOne> SubOnes { get; set; }
		public DbSet<SubTwo> SubTwos { get; set; }
		public DbSet<SubThree> SubThrees { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<LookupType> LookupTypes { get; set; }
		public DbSet<LookupItem> LookupItems { get; set; }

		public CatalogContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GroupConfiguration());
			modelBuilder.ApplyConfiguration(new SubOneConfiguration());
			modelBuilder.ApplyConfiguration(new SubTwoConfiguration());
			modelBuilder.ApplyConfiguration(new SubThreeConfiguration());
			modelBuilder.ApplyConfiguration(new ItemConfiguration());
			modelBuilder.ApplyConfiguration(new LookupTypeConfiguration());
			modelBuilder.ApplyConfiguration(new LookupItemConfiguration());

			modelBuilder.Entity<User>().HasData(new User
			{
				Id = "1",
				Username = "admin",
				PasswordHash = "$2a$11$BhE4mnYVxUVDHHQpAiLeWeivEziH9/M9DF.UnGFZqp7WMr2ag7ki2",
				Role = "Admin"
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
