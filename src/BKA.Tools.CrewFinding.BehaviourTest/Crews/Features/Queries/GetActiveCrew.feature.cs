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
namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Features.Queries
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class GetAnActiveCrewByIdentificationCodeFeature : object, Xunit.IClassFixture<GetAnActiveCrewByIdentificationCodeFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "GetActiveCrew.feature"
#line hidden
        
        public GetAnActiveCrewByIdentificationCodeFeature(GetAnActiveCrewByIdentificationCodeFeature.FixtureData fixtureData, BKA_Tools_CrewFinding_BehaviourTest_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Crews/Features/Queries", "Get an active crew by identification code", null, ProgrammingLanguage.CSharp, featureTags);
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
        
        [Xunit.SkippableFactAttribute(DisplayName="Obtain an active crew by identification code")]
        [Xunit.TraitAttribute("FeatureTitle", "Get an active crew by identification code")]
        [Xunit.TraitAttribute("Description", "Obtain an active crew by identification code")]
        public void ObtainAnActiveCrewByIdentificationCode()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Obtain an active crew by identification code", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewId",
                            "CaptainHandle",
                            "CreatedAgoHours",
                            "MaxCrewSize",
                            "System",
                            "PlanetarySystem",
                            "PlanetMoon",
                            "Location",
                            "Description",
                            "Activity",
                            "CurrentCrewSize"});
                table5.AddRow(new string[] {
                            "1234",
                            "Rowan",
                            "1",
                            "4",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting",
                            "4"});
                table5.AddRow(new string[] {
                            "3124",
                            "Ada",
                            "3",
                            "5",
                            "Terra",
                            "Sol",
                            "Terra",
                            "New Austin",
                            "Space explorers",
                            "Exploration",
                            "3"});
#line 4
        testRunner.Given("there is the following crews in the system", ((string)(null)), table5, "Given ");
#line hidden
#line 8
        testRunner.When("I want to obtain the crew with identification code \"1234\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewId",
                            "CaptainHandle",
                            "CreatedAgoHours",
                            "MaxCrewSize",
                            "System",
                            "PlanetarySystem",
                            "PlanetMoon",
                            "Location",
                            "Description",
                            "Activity",
                            "CurrentCrewSize"});
                table6.AddRow(new string[] {
                            "1234",
                            "Rowan",
                            "1",
                            "4",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting",
                            "4"});
#line 9
        testRunner.Then("I should get the following crew", ((string)(null)), table6, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Obtain an active crew by identification code that does not exist")]
        [Xunit.TraitAttribute("FeatureTitle", "Get an active crew by identification code")]
        [Xunit.TraitAttribute("Description", "Obtain an active crew by identification code that does not exist")]
        public void ObtainAnActiveCrewByIdentificationCodeThatDoesNotExist()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Obtain an active crew by identification code that does not exist", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewId",
                            "CaptainHandle",
                            "CreatedAgoHours",
                            "MaxCrewSize",
                            "System",
                            "PlanetarySystem",
                            "PlanetMoon",
                            "Location",
                            "Description",
                            "Activity",
                            "CurrentCrewSize"});
                table7.AddRow(new string[] {
                            "1234",
                            "Rowan",
                            "1",
                            "4",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting",
                            "4"});
                table7.AddRow(new string[] {
                            "3124",
                            "Ada",
                            "3",
                            "5",
                            "Terra",
                            "Sol",
                            "Terra",
                            "New Austin",
                            "Space explorers",
                            "Exploration",
                            "3"});
#line 14
        testRunner.Given("there is the following crews in the system", ((string)(null)), table7, "Given ");
#line hidden
#line 18
        testRunner.When("I attempt to obtain the crew with identification code \"999999\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 19
        testRunner.Then("I should get an error message indicating that the crew does not exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
                GetAnActiveCrewByIdentificationCodeFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                GetAnActiveCrewByIdentificationCodeFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
