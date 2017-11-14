using Autofac;
using DFlow.Budget.App.Features;
using DFlow.Budget.Core.Model;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DFlow.Budget.Specs.Bindings
{
    [Binding]
    public sealed class BudgetSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public BudgetSteps(
            ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"we are working with tenant ""(.*)"" which has no data")]
        public async Task GivenWeAreWorkingWithTenantWhichHasNoData(string name)
        {
            List<ValidationResult> errors;

            var features = Resolve<TenantFeatures>();

            Tenant tenant = await features.FindTenantByNameAsync(name);

            if (tenant != null)
            {
                errors = await features.RemoveTenantAsync(tenant);
                errors.Should().BeEmpty();
            }

            tenant = new Tenant { Name = name };
            errors = await features.AddTenantAsync(tenant);
            errors.Should().BeEmpty();
        }

        [Then(@"I can view the following budget classes;")]
        public void ThenICanViewTheFollowingBudgetClasses(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I add budget classes:")]
        public void WhenIAddBudgetClasses(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        private T Resolve<T>()
        {
            return _scenarioContext.Get<ILifetimeScope>(nameof(BudgetHooks.Scope)).Resolve<T>();
        }
    }
}
