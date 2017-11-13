using Autofac;
using DFlow.Budget.Data.Services;

namespace DFlow.Budget.Setup
{
	public class BudgetContainerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<BudgetDbContext>()
				.InstancePerLifetimeScope();
		}
	}
}
