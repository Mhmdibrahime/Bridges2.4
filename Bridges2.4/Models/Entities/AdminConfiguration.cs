using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bridges2._4.Models.Entities
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.AdminID);
            builder.Property(x=>x.AdminID).ValueGeneratedNever();
            builder.Property(a => a.Username).IsRequired();
            builder.Property(a => a.Password).IsRequired();
        }
    }

}
