using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bridges2._4.Models.Entities
{
    public class TrainConfiguration : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(t => t.TrainID);
            builder.Property(t => t.TrainName).IsRequired();
            builder.Property(t => t.TrainNumber).IsRequired();
        }
    }

}
