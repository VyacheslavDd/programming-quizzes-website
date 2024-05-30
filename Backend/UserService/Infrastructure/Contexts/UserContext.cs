using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Contexts
{
	public class UserContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }

		public UserContext(DbContextOptions<UserContext> options) : base(options)
		{
			Database.Migrate();
		}
	}
}
