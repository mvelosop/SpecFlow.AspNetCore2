using Autofac;
using DFlow.Budget.App.Features;
using DFlow.Budget.Data.Services;

namespace DFlow.Budget.Setup
{
    public class BudgetContainerModule : Module
    {
        private readonly BudgetDbSetup _dbSetup;

        public BudgetContainerModule(
            BudgetDbSetup dbSetup)
        {
            _dbSetup = dbSetup;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => _dbSetup.CreateDbContext())
                .InstancePerLifetimeScope();

            builder.RegisterType<TenantFeatures>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BudgetFeatures>()
                .InstancePerLifetimeScope();
        }
    }
}
