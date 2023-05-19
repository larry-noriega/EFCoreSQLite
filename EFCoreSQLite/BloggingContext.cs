// Ignore Spelling: Db

using EFCoreSQLite.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSQLite
{
	public class BloggingContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

		public string DbPath { get; }

		public BloggingContext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = Path.Join(path, "blogging.db");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
	}
}
