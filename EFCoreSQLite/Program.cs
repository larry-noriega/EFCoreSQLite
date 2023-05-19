namespace EFCoreSQLite
{
	class Program
	{
		static void Main()
		{
			using var db = new BloggingContext();
		}
	}
}