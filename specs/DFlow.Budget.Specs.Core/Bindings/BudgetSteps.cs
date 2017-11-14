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
        public void GivenWeAreWorkingWithTenantWhichHasNoData(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I add budget classes:")]
        public void WhenIAddBudgetClasses(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I can view the following budget classes;")]
        public void ThenICanViewTheFollowingBudgetClasses(Table table)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
