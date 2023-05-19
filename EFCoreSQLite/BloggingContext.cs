// Ignore Spelling: Db

using EFCoreSQLite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreSQLite
{
	public class BloggingContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite("Data Source=db.sqlite");
	}
}
