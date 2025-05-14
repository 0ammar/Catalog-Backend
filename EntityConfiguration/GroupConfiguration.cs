using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasMaxLength(5)
                .ValueGeneratedNever();

            builder.Property(u => u.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(u => u.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Name)
                .IsRequired() 
                .IsUnicode(true)
                .HasMaxLength(70);

            builder.HasIndex(x => x.Name)
                .IsUnique(false);

            builder.Property(x => x.Image)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.HasMany<SubOne>()
                .WithOne()
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
