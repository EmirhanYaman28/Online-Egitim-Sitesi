using denemeKnowling.Models;
using Microsoft.EntityFrameworkCore;

namespace denemeKnowling.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}
        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<Egitmen> Egitmen { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
