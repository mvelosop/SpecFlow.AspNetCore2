using Autofac;
using Autofac.Extensions.DependencyInjection;
using DFlow.Budget.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;
using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow;

namespace DFlow.Budget.Specs.Bindings
{
    [Binding]
    public sealed class BudgetHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public static IContainer Container { get; set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            var builder = new ContainerBuilder();

            builder.Populate(services);
            ConfigureContainer(builder);
            Container = builder.Build();

            ConfigureDabatase();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new BudgetContainerModule());
        }

        private static void ConfigureDabatase()
        {
            var connectionString = "Server=localhost; Initial Catalog=SpecFlow.AspNetCore2; Trusted_Connection=True; MultipleActiveResultSets=true";

            var dbSetup = new BudgetDbSetup(connectionString);

            dbSetup.ConfigureDatabase();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
