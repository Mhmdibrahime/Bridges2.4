using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bridges2._4.Models.Entities
{
    public class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(s => s.StationID);
            builder.Property(s => s.StationName).IsRequired();
            builder.Property(s => s.Location).IsRequired();
        }
    }

}
