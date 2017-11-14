using Autofac;
using DFlow.Budget.App;
using DFlow.Budget.App.Features;
using DFlow.Budget.Core.Model;
using DFlow.Budget.Specs.Helpers;
using Domion.Testing.Assertions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        [Then(@"I can't duplicate budget class names:")]
        public async Task ThenICanTDuplicateBudgetClassNames(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            List<BudgetClass> entityList = table.CreateSet<BudgetClass>().ToList();

            foreach (BudgetClass budgetClass in entityList)
            {
                var errors = await features.AddBudgetClassAsync(budgetClass);
                errors.Should().ContainErrorMessage(BudgetClassFeatures.DuplicateByNameError);
            }
        }

        [Then(@"I get the following budget classes")]
        public async Task ThenIGetTheFollowingBudgetClasses(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            var budgetClassList = await features.QueryBudgetClasses().ToListAsync();

            table.CompareToSet(budgetClassList);
        }

        [Then(@"I get the following budget items for class ""(.*)"":")]
        public async Task ThenIGetTheFollowingBudgetItemsForClass(string name, Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            BudgetClass entity = await features.FindBudgetClassByNameAsync(name);

            table.CompareToSet(entity.BudgetItems);
        }

        [When(@"I add budget classes:")]
        [Given(@"I've added budget classes:")]
        public async Task WhenIAddBudgetClasses(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            List<BudgetClass> entityList = table.CreateSet<BudgetClass>().ToList();

            foreach (BudgetClass budgetClass in entityList)
            {
                var errors = await features.AddBudgetClassAsync(budgetClass);
                errors.Should().BeEmpty();
            }
        }

        [When(@"I add the following budget items:")]
        public async Task WhenIAddTheFollowingBudgetItems(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            var dataSet = table.CreateSet<BudgetItemData>();

            foreach (var group in dataSet.GroupBy(bid => bid.BudgetClass))
            {
                var budgetClass = await features.FindBudgetClassByNameAsync(group.Key);

                budgetClass.Should().NotBeNull($@"because BudgetClass ""{group.Key}"" MUST exist");

                var mapper = new BudgetItemDataMapper();

                var items = group.Select(bid => mapper.CreateEntity(bid));

                var errors = await features.AddBudgetItemsRangeAsync(budgetClass, items);

                errors.Should().BeEmpty();
            }
        }

        [When(@"I delete the original budget classes:")]
        public async Task WhenIDeleteTheOriginalBudgetClasses(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            List<BudgetClassData> dataList = table.CreateSet<BudgetClassData>().ToList();

            foreach (BudgetClassData data in dataList)
            {
                var entity = await features.FindBudgetClassByNameAsync(data.FindName);
                var errors = await features.RemoveBudgetClassAsync(entity);
                errors.Should().BeEmpty();
            }
        }

        [When(@"I modify the original budget classes:")]
        public async Task WhenIModifyTheOriginalBudgetClassesAsync(Table table)
        {
            var features = Resolve<BudgetClassFeatures>();

            List<BudgetClassData> dataList = table.CreateSet<BudgetClassData>().ToList();

            foreach (BudgetClassData data in dataList)
            {
                var entity = await features.FindBudgetClassByNameAsync(data.FindName);

                entity.Name = data.Name;
                entity.SortOrder = data.SortOrder;
                entity.TransactionType = data.TransactionType;

                var errors = await features.ModifyBudgetClassAsync(entity);

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
