using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DFlow.Budget.App;
using DFlow.Budget.Setup;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace DFlow.Budget.Specs.Bindings
{
    [Binding]
    public sealed class BudgetHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private const string ConnectionString = "Server=localhost; Initial Catalog=SpecFlow.AspNetCore2.Specs; Trusted_Connection=True; MultipleActiveResultSets=true";
        private readonly ScenarioContext _scenarioContext;

        public BudgetHooks(
            ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public static IContainer Container { get; private set; }

        public static BudgetDbSetup DbSetup { get; private set; }

        public ILifetimeScope Scope { get; private set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            DbSetup = ConfigureDabatase();

            var services = new ServiceCollection();

            ConfigureServices(services);

            var builder = new ContainerBuilder();

            builder.Populate(services);
            ConfigureContainer(builder);
            Container = builder.Build();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TryGetValue(nameof(Scope), out ILifetimeScope scope))
            {
                scope?.Dispose();

                _scenarioContext.Remove(nameof(Scope));
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _scenarioContext.Set(Container, nameof(Container));
        }

        [BeforeStep]
        public void BeforeStep()
        {
            Scope = GetLifetimeScope();

            _scenarioContext.Set(Scope, nameof(Scope));
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new BudgetContainerModule(DbSetup));
        }

        private static BudgetDbSetup ConfigureDabatase()
        {
            var dbSetup = new BudgetDbSetup(ConnectionString);

            dbSetup.ConfigureDatabase();

            return dbSetup;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // TODO: Configure services
        }

        private ILifetimeScope GetLifetimeScope()
        {
            return _scenarioContext.Get<IContainer>(nameof(Container))
                .BeginLifetimeScope(builder =>
                {
                    builder.Register(c => GetSessionContext());
                });
        }

        private SessionContext GetSessionContext()
        {
            if (_scenarioContext.TryGetValue(nameof(SessionContext), out SessionContext sessionContext))
            {
                return sessionContext;
            }

            throw new InvalidOperationException($"{nameof(SessionContext)} not set!");
        }
    }
}
