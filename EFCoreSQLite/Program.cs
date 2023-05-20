using EFCoreSQLite.Entities;
using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace EFCoreSQLite
{
	class Program
	{
		private static Blog? _blog;
		private static readonly BloggingContext _db = new();

		static void Main()
		{
			JsonSerializerOptions jsonOptions = new()
			{
				WriteIndented = true,
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			string filePath = $"{_db.DbPath}";

			//Database file path
			Console.WriteLine($"Database path: {filePath}.");
			Console.WriteLine($"Opening database...");


			//TODO: Add Breakpoint here
			//Open Database
			Process openDatabase = new()
			{
				StartInfo = new(@filePath)
				{
					UseShellExecute = true
				}
			};
			openDatabase.Start();
			
			_blog = new() { Url = "http://blogs.msdn.com/adonet" };


			//TODO: Add Breakpoint here
			// Create
			Console.WriteLine("Inserting a new blog");

			_db.Add(_blog);
			_db.SaveChanges();		

			Console.WriteLine("\r\n" + JsonSerializer.Serialize(_blog, jsonOptions) + "\r\n");


			//TODO: Add Breakpoint here
			// Read
			Console.WriteLine("Querying for a blog");

			_blog = _db.Blogs
				.OrderBy(b => b.BlogId)
				.First();

			Console.WriteLine("\r\n" +  JsonSerializer.Serialize(_blog, jsonOptions) + "\r\n");


			//TODO: Add Breakpoint here
			//Update
			Console.WriteLine("Updating the blog and adding a post");
			_blog.Url = "https://devblogs.microsoft.com/dotnet";
			_blog.Posts.Add(
				new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });

			_db.SaveChanges();

			_blog = _db.Blogs
				.Where(b => b.BlogId == _blog.BlogId)
				.FirstOrDefault();

			Console.WriteLine("\r\n" + JsonSerializer.Serialize(_blog, jsonOptions) + "\r\n");


			//TODO: Add Breakpoint here
			// Delete
			int? blogID = _blog?.BlogId;

			Console.WriteLine("Delete the blog");

			_db.Remove(_blog);
			_db.SaveChanges();

			_blog = _db.Blogs
				.Where(b => b.BlogId == blogID)
				.FirstOrDefault();

			Console.WriteLine("\r\n" + $"Checking existence for a blogId: {blogID}");			

			Console.WriteLine("\r\n" + _blog is null ? "Blog was found: " : "Blog was not found: " + JsonSerializer.Serialize(_blog, jsonOptions) + "\r\n" );
		}
	}
}