// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SpecFlow.GeneratedTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class Feature_1_ManageBudgetClassesFeature : Xunit.IClassFixture<Feature_1_ManageBudgetClassesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "[1] - ManageBudgetClasses.feature"
#line hidden
        
        public Feature_1_ManageBudgetClassesFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Feature - 1 - ManageBudgetClasses", "\tAs a master user\r\n\tI need to manage budget classes\r\n\tTo keep control of my budge" +
                    "t", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void SetFixture(Feature_1_ManageBudgetClassesFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Scenario - 1.1 - Add budget classes")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature - 1 - ManageBudgetClasses")]
        [Xunit.TraitAttribute("Description", "Scenario - 1.1 - Add budget classes")]
        public virtual void Scenario_1_1_AddBudgetClasses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario - 1.1 - Add budget classes", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("we are working with tenant \"1.1 - Add budget classes\" which has no data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "SortOrder",
                        "TransactionType"});
            table1.AddRow(new string[] {
                        "Income",
                        "1",
                        "Income"});
            table1.AddRow(new string[] {
                        "Housing",
                        "2",
                        "Expense"});
            table1.AddRow(new string[] {
                        "Food",
                        "3",
                        "Expense"});
            table1.AddRow(new string[] {
                        "Transportation",
                        "4",
                        "Expense"});
            table1.AddRow(new string[] {
                        "Entertainment",
                        "5",
                        "Expense"});
#line 10
 testRunner.When("I add budget classes:", ((string)(null)), table1, "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "SortOrder",
                        "TransactionType"});
            table2.AddRow(new string[] {
                        "Income",
                        "1",
                        "Income"});
            table2.AddRow(new string[] {
                        "Housing",
                        "2",
                        "Expense"});
            table2.AddRow(new string[] {
                        "Food",
                        "3",
                        "Expense"});
            table2.AddRow(new string[] {
                        "Transportation",
                        "4",
                        "Expense"});
            table2.AddRow(new string[] {
                        "Entertainment",
                        "5",
                        "Expense"});
#line 18
 testRunner.Then("I can view the following budget classes;", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                Feature_1_ManageBudgetClassesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                Feature_1_ManageBudgetClassesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
