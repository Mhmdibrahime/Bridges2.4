using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bridges2._4.Models.Entities
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.ScheduleID);
            builder.Property(s => s.Fare)
        .HasColumnType("decimal(18,2)");
            builder.HasOne(s => s.Train)
                   .WithMany(t => t.Schedules)
                   .HasForeignKey(s => s.TrainID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.DepartureStation)
                   .WithMany()
                   .HasForeignKey(s => s.DepartureStationID)
                   .OnDelete(DeleteBehavior.NoAction); // Assuming no station should be deleted if referenced by a schedule

            builder.HasOne(s => s.ArrivalStation)
                   .WithMany()
                   .HasForeignKey(s => s.ArrivalStationID)
                   .OnDelete(DeleteBehavior.NoAction); // Assuming no station should be deleted if referenced by a schedule
        }
    }

}
