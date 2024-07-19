using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bridges2._4.Models.Entities
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingID);

            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Schedule)
                   .WithMany(s => s.Bookings)
                   .HasForeignKey(b => b.ScheduleID)
                   .OnDelete(DeleteBehavior.Cascade);

            // Assuming Payment is optional for a Booking
            builder.HasOne(b => b.Payment)
                   .WithOne(p => p.Booking)
                   .HasForeignKey<Payment>(p => p.BookingID)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);
        }
    }

}
