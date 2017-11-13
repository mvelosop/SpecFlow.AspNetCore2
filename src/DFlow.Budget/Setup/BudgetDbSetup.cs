using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DFlow.Budget.Setup
{
	public class BudgetDbSetup
	{
		private readonly string _connectionString;

		private DbContextOptions<BudgetDbContext> _options;

		public BudgetDbSetup(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void ConfigureDatabase()
		{
			var optionBuilder = new DbContextOptionsBuilder<BudgetDbContext>();

			// This lock avoids conflicts on DB creation, specially during parallel integration tests
			lock (_connectionString)
			{
				optionBuilder.UseSqlServer(_connectionString);

				// Uncomment this option to show data values on SQL logs,
				// this represents a security risk so use with caution
				optionBuilder.EnableSensitiveDataLogging();

				_options = optionBuilder.Options;

				var tokens = _connectionString.Split(';');

				string server = tokens.FirstOrDefault(s => s.StartsWith("Server")) ??
								tokens.FirstOrDefault(s => s.StartsWith("Data Source"));

				Console.WriteLine($"MainDbContext - Server: {server}");

				using (var dbContext = CreateDbContext())
				{
					dbContext.Database.Migrate();
				}
			}
		}

		public BudgetDbContext CreateDbContext()
		{
			if (_options == null) throw new InvalidOperationException($"Must run {GetType().Name}.{nameof(ConfigureDatabase)} first!");

			return new BudgetDbContext(_options);
		}
	}
}
