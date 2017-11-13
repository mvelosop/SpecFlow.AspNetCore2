using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DFlow.Cli
{
	public class BudgetDbContextFactory : IDesignTimeDbContextFactory<BudgetDbContext>
	{
		public BudgetDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BudgetDbContext>();

			builder.UseSqlServer("x");

			return new BudgetDbContext(builder.Options);
		}
	}
}
