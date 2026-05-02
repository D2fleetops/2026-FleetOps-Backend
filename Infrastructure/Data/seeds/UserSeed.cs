using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
	public static class UserSeed
	{
		public static void SeedUsers(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = 1,
					FullName = "FleetOps Test Driver",
					Email = "driver@fleetops.local",
					PasswordHash = "$2a$11$wvBoSd9TRmWoarbcfmSrwu.AeqYvmWSP1o.fdUrj88yS33TSJL2Dm",
					RoleId = 3,
					CreatedAt = new DateTimeOffset(new DateTime(2026, 5, 2, 11, 59, 13, DateTimeKind.Utc))
				}
			);
		}
	}
}
