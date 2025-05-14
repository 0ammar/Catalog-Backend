using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Backend.EntityConfiguration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.ItemNo);
            builder.Property(x => x.ItemNo)
                   .ValueGeneratedNever();
            builder.HasIndex(x => x.ItemNo)
                   .IsUnique();

            // Name and Description
            builder.Property(x => x.Name)
                   .HasMaxLength(255)
                   .IsRequired()
                   .IsUnicode(true);

            builder.HasIndex(x => x.Name).IsUnique(false);

            builder.Property(x => x.Description)
                   .IsRequired(false);

            // JSON Images
            builder.Property(x => x.Images)
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!)!
                   )
                   .IsRequired(false);

            // Status
            builder.Property(x => x.StatusId)
                   .HasDefaultValue(2);

            builder.HasOne(i => i.Status)
                   .WithMany()
                   .HasForeignKey(i => i.StatusId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Group/SubOne/SubTwo/SubThree IDs
            builder.Property(x => x.GroupId).IsRequired();
            builder.Property(x => x.SubOneId).IsRequired();
            builder.Property(x => x.SubTwoId).IsRequired(false);
            builder.Property(x => x.SubThreeId).IsRequired(false);

            // Foreign Keys (optional)
            builder.HasOne<Group>()
                   .WithMany()
                   .HasForeignKey(x => x.GroupId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<SubOne>()
                   .WithMany()
                   .HasForeignKey(x => x.SubOneId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<SubTwo>()
                   .WithMany()
                   .HasForeignKey(x => x.SubTwoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<SubThree>()
                   .WithMany()
                   .HasForeignKey(x => x.SubThreeId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Metadata
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.CreationDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
