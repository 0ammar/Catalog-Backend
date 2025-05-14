using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.EntityConfiguration
{
    public class LookupTypeConfiguration : IEntityTypeConfiguration<LookupType>
    {
        public void Configure(EntityTypeBuilder<LookupType> builder)
        {
            builder.ToTable("LookupTypes");

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasMaxLength(5)
                   .ValueGeneratedNever();

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreationDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Name)
                   .IsUnicode(true)
                   .IsRequired()
                   .HasMaxLength(70);

            builder.HasMany<LookupItem>()
                   .WithOne()
                   .HasForeignKey(x => x.LookupTypeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new LookupType
                {
                    Id = "1",
                    Name = "ItemStatus",
                }
            );
        }
    }
}
