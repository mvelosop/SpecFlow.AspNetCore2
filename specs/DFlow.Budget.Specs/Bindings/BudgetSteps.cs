using Autofac;
using DFlow.Budget.App;
using DFlow.Budget.App.Features;
using DFlow.Budget.Core.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domion.Testing.Assertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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

        [Then(@"I can't duplicate budget class names:")]
        public async Task ThenICanTDuplicateBudgetClassNames(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            List<BudgetClass> budgetClassList = table.CreateSet<BudgetClass>().ToList();

            foreach (BudgetClass budgetClass in budgetClassList)
            {
                var errors = await features.AddBudgetClassAsync(budgetClass);
                errors.Should().ContainErrorMessage(BudgetClassFeatures.DuplicateByNameError);
            }
        }

        [Given(@"we are working with a new scenario tenant context")]
        public async Task GivenWeAreWorkingWithANewScenarioTenantContext()
        {
            string name = _scenarioContext.ScenarioInfo.Title;

            await ArrangeScenarioTenantContext(name);
        }

        [Given(@"we are working with tenant ""(.*)"" which has no data")]
        public async Task GivenWeAreWorkingWithTenantWhichHasNoData(string name)
        {
            await ArrangeScenarioTenantContext(name);
        }

        [Then(@"I can get the following budget classes")]
        public async Task ThenICanGetTheFollowingBudgetClasses(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            var budgetClassList = await features.QueryBudgetClasses().ToListAsync();

            table.CompareToSet(budgetClassList);
        }

        [When(@"I add budget classes:")]
        [Given(@"I've added budget classes:")]
        public async Task WhenIAddBudgetClasses(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            List<BudgetClass> budgetClassList = table.CreateSet<BudgetClass>().ToList();

            foreach (BudgetClass budgetClass in budgetClassList)
            {
                var errors = await features.AddBudgetClassAsync(budgetClass);
                errors.Should().BeEmpty();
            }
        }

        private async Task ArrangeScenarioTenantContext(string name)
        {
            List<ValidationResult> errors;

            var tenantFeatures = Resolve<TenantFeatures>();

            Tenant tenant = await tenantFeatures.FindTenantByNameAsync(name);

            if (tenant != null)
            {
                var appFeatures = Resolve<AppFeatures>();

                errors = await appFeatures.PurgeTenantAsync(tenant);
                errors.Should().BeEmpty();
            }

            tenant = new Tenant { Name = name };
            errors = await tenantFeatures.AddTenantAsync(tenant);
            errors.Should().BeEmpty();

            var sessionContext = new SessionContext(tenant);

            _scenarioContext.Set(sessionContext, nameof(SessionContext));
        }

        private ILifetimeScope GetScope()
        {
            return _scenarioContext.Get<ILifetimeScope>(nameof(BudgetHooks.Scope));
        }

        private T Resolve<T>()
        {
            return GetScope().Resolve<T>();
        }
    }
}
