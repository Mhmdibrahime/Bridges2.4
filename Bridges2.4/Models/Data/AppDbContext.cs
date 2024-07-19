using Bridges2._4.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bridges2._4.Models.Data
{
	public class AppDbContext:IdentityDbContext<ApplicationUser>
	{
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options):base(options) { }
        
        public DbSet<User> users {  get; set; }
		public DbSet<Admin> admins { get; set; }
		public DbSet<Station> stations { get; set; }	
		public DbSet<Booking> bookings { get; set; }
		public DbSet<Schedule> schedules { get; set; }
		public DbSet<Payment> payment { get; set; }
		public DbSet<Train> train { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			var constr = config.GetSection("constr").Value;
			optionsBuilder.UseSqlServer(constr);
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
		}

	}
}
