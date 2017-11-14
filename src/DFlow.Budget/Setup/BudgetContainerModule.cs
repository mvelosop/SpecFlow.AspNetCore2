using Autofac;
using DFlow.Budget.App.Features;

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

            builder.RegisterType<BudgetClassFeatures>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppFeatures>()
                .InstancePerLifetimeScope();
        }
    }
}
