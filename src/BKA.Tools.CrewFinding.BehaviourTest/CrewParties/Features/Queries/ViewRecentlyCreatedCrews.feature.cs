﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Features.Queries
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ViewRecentlyCreatedCrewsFeature : object, Xunit.IClassFixture<ViewRecentlyCreatedCrewsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ViewRecentlyCreatedCrews.feature"
#line hidden
        
        public ViewRecentlyCreatedCrewsFeature(ViewRecentlyCreatedCrewsFeature.FixtureData fixtureData, BKA_Tools_CrewFinding_BehaviourTest_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "CrewParties/Features/Queries", "View recently created Crews", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="View recently created crews")]
        [Xunit.TraitAttribute("FeatureTitle", "View recently created Crews")]
        [Xunit.TraitAttribute("Description", "View recently created crews")]
        public void ViewRecentlyCreatedCrews()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View recently created crews", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
        testRunner.Given("I am a player named \"Allan\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 5
        testRunner.And("the system is configured to get the crews created in the last \"5\" hour", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewPartyName",
                            "CaptainHandle",
                            "CreatedAgoHours",
                            "MaxCrewSize",
                            "Languages",
                            "System",
                            "PlanetarySystem",
                            "PlanetMoon",
                            "Location",
                            "Description",
                            "Activity"});
                table3.AddRow(new string[] {
                            "The Stellar Hunters",
                            "Rowan",
                            "1",
                            "4",
                            "ES, EN",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting"});
                table3.AddRow(new string[] {
                            "The Galactic Voyagers",
                            "Ada",
                            "3",
                            "5",
                            "EN, DE",
                            "Terra",
                            "Sol",
                            "Terra",
                            "New Austin",
                            "Space explorers",
                            "Exploration"});
                table3.AddRow(new string[] {
                            "The Lunar Marauders",
                            "Kai",
                            "5",
                            "3",
                            "EN, FR",
                            "Hurston",
                            "Stanton",
                            "Ariel",
                            "Lorville",
                            "Lunar miners",
                            "Mining"});
                table3.AddRow(new string[] {
                            "The Space Pioneers",
                            "Eve",
                            "6",
                            "6",
                            "ES, PT",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Port Olisar",
                            "Intergalactic pioneers",
                            "Trade"});
#line 6
        testRunner.And("there is the following crews in the system", ((string)(null)), table3, "And ");
#line hidden
#line 12
        testRunner.When("I view the recently created crews", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewPartyName",
                            "CaptainHandle",
                            "CreatedAgoHours",
                            "MaxCrewSize",
                            "Languages",
                            "System",
                            "PlanetarySystem",
                            "PlanetMoon",
                            "Location",
                            "Description",
                            "Activity"});
                table4.AddRow(new string[] {
                            "The Stellar Hunters",
                            "Rowan",
                            "1",
                            "4",
                            "ES, EN",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting"});
                table4.AddRow(new string[] {
                            "The Galactic Voyagers",
                            "Ada",
                            "3",
                            "5",
                            "EN, DE",
                            "Terra",
                            "Sol",
                            "Terra",
                            "New Austin",
                            "Space explorers",
                            "Exploration"});
#line 13
        testRunner.Then("I should see the following crews", ((string)(null)), table4, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ViewRecentlyCreatedCrewsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ViewRecentlyCreatedCrewsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
