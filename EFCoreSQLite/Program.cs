using EFCoreSQLite.Entities;
using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace EFCoreSQLite
{
	class Program
	{
		static void Main()
		{
			using var db = new BloggingContext();

			var jsonOptions = new JsonSerializerOptions {
				WriteIndented = true,
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			string filePath = $"{db.DbPath}";

			Console.WriteLine($"Database path: {filePath}.");

			//TODO: Add Breakpoint here
			//Open Database
			Process openDatabase = new()
			{
				StartInfo = new ProcessStartInfo(@filePath)
				{
					UseShellExecute = true
				}
			};

			openDatabase.Start();
			
			Blog? blog = new() { Url = "http://blogs.msdn.com/adonet" };


			//TODO: Add Breakpoint here
			// Create
			Console.WriteLine("Inserting a new blog");

			db.Add(blog);
			db.SaveChanges();

			string jsonString = JsonSerializer.Serialize(blog, jsonOptions);			

			Console.WriteLine("\r\n" + jsonString + "\r\n");


			//TODO: Add Breakpoint here
			// Read
			Console.WriteLine("Querying for a blog");

			blog = db.Blogs
				.OrderBy(b => b.BlogId)
				.First();

			jsonString = JsonSerializer.Serialize(blog, jsonOptions);

			Console.WriteLine("\r\n" + jsonString + "\r\n");


			//TODO: Add Breakpoint here
			//Update
			Console.WriteLine("Updating the blog and adding a post");
			blog.Url = "https://devblogs.microsoft.com/dotnet";
			blog.Posts.Add(
				new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });

			db.SaveChanges();

			blog = db.Blogs
				.Where(b => b.BlogId == blog.BlogId)
				.FirstOrDefault();

			jsonString = JsonSerializer.Serialize(blog, jsonOptions);

			Console.WriteLine("\r\n" + jsonString + "\r\n");


			//TODO: Add Breakpoint here
			// Delete
			int? blogID = blog.BlogId;

			Console.WriteLine("Delete the blog");
			db.Remove(blog);
			db.SaveChanges();

			blog = db.Blogs
				.Where(b => b.BlogId == blogID)
				.FirstOrDefault();

			jsonString = JsonSerializer.Serialize(blog, jsonOptions);

			Console.WriteLine("\r\n" + $"Checking existence for a blogId: {blogID}");			

			Console.WriteLine("\r\n" + blog is null ? "Blog was found: " : "Blog was not found: " + jsonString + "\r\n" );
		}
	}
}